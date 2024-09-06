// <copyright file="GotenbergService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.library.gotenberg;

using EnterpriseStartup.Telemetry;
using Gotenberg.Sharp.API.Client;
using Gotenberg.Sharp.API.Client.Domain.Builders;
using Gotenberg.Sharp.API.Client.Domain.Builders.Faceted;
using Gotenberg.Sharp.API.Client.Domain.Requests.Facets;
using Microsoft.Extensions.Logging;
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
                .SetScale(.95)
                .LandScape());

        var request = await builder.BuildAsync();
        var retVal = await client.UrlToPdfAsync(request);
        telemeter.CaptureMetric(MetricType.Counter, retVal.Length, "pdf_file_size");
        logger.LogInformation("Pdf of {Bytes} bytes converted from url: {Url}", retVal.Length, url);

        return retVal;
    }

    /// <inheritdoc/>
    public async Task<Stream> FromHtml(Stream htmlContent)
    {
        var builder = new HtmlRequestBuilder()
            .AddDocument(doc => doc.SetBody(new ContentItem(htmlContent)))
            .WithDimensions(dims => dims
                .SetPaperSize(PaperSizes.A4)
                .SetMargins(Margins.None)
                .SetScale(.95));

        var request = await builder.BuildAsync();
        var retVal = await client.HtmlToPdfAsync(request);
        telemeter.CaptureMetric(MetricType.Counter, retVal.Length, "pdf_file_size");
        logger.LogInformation("Pdf of {Bytes} bytes converted from html", retVal.Length);

        return retVal;
    }

    /// <inheritdoc/>
    public async Task<Stream> FromOfficeDoc(Stream officeDoc)
    {
        var builder = new MergeOfficeBuilder()
            .WithAssets(b => b.AddItem("office-doc.docx", officeDoc))
            .SetPdfFormat(PdfFormats.A2b)
            .UseNativePdfFormat();

        var retVal = await client.MergeOfficeDocsAsync(builder);
        telemeter.CaptureMetric(MetricType.Counter, retVal.Length, "pdf_file_size");
        logger.LogInformation("Pdf of {Bytes} bytes converted from office doc", retVal.Length);

        return retVal;
    }
}