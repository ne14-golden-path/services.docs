﻿// <copyright file="PdfConversionSucceededProducer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using ne14.library.rabbitmq.Vendor;
using ne14.library.startup_extensions.Mq;
using ne14.library.startup_extensions.Telemetry;

/// <summary>
/// Produces <see cref="PdfConversionSucceededMessage"/> mq messages.
/// </summary>
public class PdfConversionSucceededProducer(
    IRabbitMqSession session,
    ITelemeter telemeter,
    ILogger<TracedMqProducer<PdfConversionSucceededMessage>> logger)
        : TracedMqProducer<PdfConversionSucceededMessage>(session, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-succeeded";
}
