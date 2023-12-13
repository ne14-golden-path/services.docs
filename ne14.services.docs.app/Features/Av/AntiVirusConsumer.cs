// <copyright file="AntiVirusConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.app.Features.Av;

using ne14.library.message_contracts.Docs;
using ne14.services.docs.business.Features.Av;

/// <summary>
/// Antivirus mq consumer.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AntiVirusMqConsumer"/> class.
/// </remarks>
/// <param name="scanner">The scanner.</param>
public class AntiVirusMqConsumer(IAntiVirusScanner scanner)
{
    /// <summary>
    /// Consumes message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>Async task.</returns>
    public async Task Consume(PdfConversionRequired message)
    {
        await Task.CompletedTask;

        ////using var str = file.OpenReadStream();
        ////await this.scanner.AssertIsClean(str);
    }
}