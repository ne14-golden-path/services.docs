// <copyright file="PdfConversionRequiredConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using EnterpriseStartup.Messaging.Abstractions.Consumer;
using EnterpriseStartup.Mq;
using EnterpriseStartup.Telemetry;
using FluentErrors.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.services.docs.business.Features.Av;
using ne14.services.docs.business.Features.Blob;
using RabbitMQ.Client;

/// <summary>
/// Consumer for pdf conversion.
/// </summary>
public class PdfConversionRequiredConsumer(
    PdfConversionSucceededProducer successMessenger,
    PdfConversionFailedProducer failureMessenger,
    IBlobRepository blobRepository,
    IAntiVirusScanner avScanner,
    IPdfConverter pdfConverter,
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionRequiredConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionRequiredMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-required";

    /// <inheritdoc/>
    public override async Task ConsumeAsync(PdfConversionRequiredMessage message, MqConsumerEventArgs args)
    {
        message.MustExist();
        var inboundRef = message.InboundBlobReference;

        try
        {
            var inputBlob = await blobRepository.DownloadAsync("triage", inboundRef);
            await avScanner.AssertIsClean(inputBlob.Content);
            var extension = new FileInfo(inputBlob.FileName).Extension.ToLowerInvariant();
            var converted = extension switch
            {
                ".html" => await pdfConverter.FromHtml(inputBlob.Content),
                ".docx" => await pdfConverter.FromOfficeDoc(inputBlob.Content),
                _ => throw new PermanentFailureException("Not supported for pdf conversion")
            };

            await inputBlob.Content.DisposeAsync();
            var outputBlob = new BlobMeta(converted, inputBlob.FileName + ".pdf");
            var outboundRef = await blobRepository.UploadAsync("converted", outputBlob);
            successMessenger.Produce(new(message.UserId, inboundRef, outboundRef));
            await blobRepository.DeleteAsync("triage", inboundRef);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "pdf conversion failed");
            if (args.MustExist().AttemptNumber == this.MaximumAttempts || ex is PermanentFailureException)
            {
                failureMessenger.Produce(new(message.UserId, inboundRef, $"{ex.GetType().Name} - {ex.Message}"));
            }

            throw;
        }
    }
}
