// <copyright file="PdfConversionRequiredConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ne14.library.fluent_errors.Extensions;
using ne14.library.message_contracts.Docs;
using ne14.library.messaging.Abstractions.Consumer;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;
using RabbitMQ.Client;

/// <summary>
/// Consumer for pdf conversion.
/// </summary>
public class PdfConversionRequiredConsumer(
    PdfConversionSucceededProducer successMessenger,
    PdfConversionFailedProducer failureMessenger,
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionRequiredConsumer> logger,
    IConfiguration config)
        : MqTracingConsumer<PdfConversionRequiredMessage>(connectionFactory, telemeter, logger, config)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-required";

    /// <inheritdoc/>
    public override Task ConsumeAsync(PdfConversionRequiredMessage message, MqConsumerEventArgs args)
    {
        message.MustExist();

        //// download blob bytes
        //// virus scan content
        //// convert the bytes to pdf
        //// upload to outbound

        var convertedOk = Random.Shared.Next() % 2 == 0;
        var inboundRef = message.InboundBlobReference;
        var outboundRef = Guid.NewGuid();

        if (convertedOk)
        {
            successMessenger.Produce(new(inboundRef, outboundRef));
        }
        else
        {
            failureMessenger.Produce(new(inboundRef, "failed, bruh"));
        }

        //// delete the original
        return Task.CompletedTask;
    }
}
