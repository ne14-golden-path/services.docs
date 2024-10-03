// <copyright file="ClamAvScanner.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.library.clamav;

using EnterpriseStartup.Telemetry;
using FluentErrors.Extensions;
using Microsoft.Extensions.Logging;
using nClam;
using ne14.services.docs.business.Features.Av;

/// <inheritdoc cref="IAntiVirusScanner"/>
/// <summary>
/// Initializes a new instance of the <see cref="ClamAvScanner"/> class.
/// </summary>
/// <param name="client">The client.</param>
/// <param name="logger">The logger.</param>
/// <param name="telemeter">The telemeter.</param>
public class ClamAvScanner(
    IClamClient client,
    ILogger<ClamAvScanner> logger,
    ITelemeter telemeter) : IAntiVirusScanner
{
    /// <inheritdoc/>
    public async Task AssertIsClean(Stream content)
    {
        content.MustExist();
        content.Seek(0, SeekOrigin.Begin);
        var scanResponse = await client.SendAndScanFileAsync(content);
        var scanResult = scanResponse.Result;
        telemeter.CaptureMetric(MetricType.Counter, content.Length, "av_file_size");
        logger.LogInformation("File scanned, length: {Bytes}, result: {Result}", content.Length, scanResult);

        scanResult.MustBe(ClamScanResults.Clean, $"Unhappy scan of {content.Length} bytes. Result: {scanResult}");
    }
}