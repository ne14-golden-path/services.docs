// <copyright file="PdfConversionFailedProducer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

using EnterpriseStartup.Mq;
using EnterpriseStartup.Telemetry;
using Microsoft.Extensions.Logging;
using ne14.library.message_contracts.Docs;
using RabbitMQ.Client;

/// <summary>
/// Produces <see cref="PdfConversionFailedMessage"/> mq messages.
/// </summary>
public class PdfConversionFailedProducer(
    IConnectionFactory connectionFactory,
    ITelemeter telemeter,
    ILogger<PdfConversionFailedProducer> logger)
        : MqTracingProducer<PdfConversionFailedMessage>(connectionFactory, telemeter, logger)
{
    /// <inheritdoc/>
    public override string ExchangeName => "pdf-conversion-failed";
}
