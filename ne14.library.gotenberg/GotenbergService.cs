﻿// <copyright file="GotenbergService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.library.gotenberg;

using Gotenberg.Sharp.API.Client;
using Gotenberg.Sharp.API.Client.Domain.Builders;
using Gotenberg.Sharp.API.Client.Domain.Builders.Faceted;
using Microsoft.Extensions.Logging;
using ne14.library.startup_extensions.Telemetry;
using ne14.services.docs.business.Features.Pdf;

/// <inheritdoc cref="IPdfConverter"/>
/// <summary>
/// Initializes a new instance of the <see cref="GotenbergService"/> class.
/// </summary>
/// <param name="client">The client.</param>
/// <param name="logger">The logger.</param>
/// <param name="telemeter">The telemeter.</param>
public class GotenbergService(
    GotenbergSharpClient client,
    ILogger<GotenbergService> logger,
    ITelemeter telemeter) : IPdfConverter
{
    /// <inheritdoc/>
    public async Task<Stream> FromUrl(string url)
    {
        var builder = new UrlRequestBuilder()
            .SetUrl(new Uri(url))
            .WithDimensions(dims => dims
                .SetPaperSize(PaperSizes.A4)
                .SetMargins(Margins.None)
                .SetScale(.90)
                .LandScape());

        var request = await builder.BuildAsync();
        var retVal = await client.UrlToPdfAsync(request);
        telemeter.CaptureMetric(MetricType.Counter, retVal.Length, "pdf_file_size");
        logger.LogInformation("Pdf of {Bytes} bytes converted from url: {Url}", retVal.Length, url);

        return retVal;
    }
}