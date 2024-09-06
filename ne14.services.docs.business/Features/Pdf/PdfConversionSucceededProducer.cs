// <copyright file="PdfConversionSucceededProducer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using EnterpriseStartup.Mq;
using EnterpriseStartup.Telemetry;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using RabbitMQ.Client;

/// <summary>
/// Produces <see cref="PdfConversionSucceededMessage"/> mq messages.
/// </summary>
public class PdfConversionSucceededProducer(
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionSucceededProducer> logger)
        : MqTracingProducer<PdfConversionSucceededMessage>(connectionFactory, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-succeeded";
}
