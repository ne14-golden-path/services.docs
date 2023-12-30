// <copyright file="PdfConversionConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.rabbitmq.Consumer;
using ne14.library.rabbitmq.Vendor;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;

/// <summary>
/// Consumer for pdf conversion.
/// </summary>
public class PdfConversionConsumer(
    IRabbitMqSession session,
    ITelemeter telemeter,
    ILogger<PdfConversionConsumer> logger)
        : TracedMqConsumer<PdfConversionRequired>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion";

    /// <inheritdoc/>
    public override async Task Consume(PdfConversionRequired message, ConsumerContext context)
    {
        await Task.CompletedTask;

        throw new ArithmeticException("duuuurrrrr");
    }
}
