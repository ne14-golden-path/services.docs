// <copyright file="PdfConsumer.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.app.Features.Pdf;

using ne14.library.message_contracts.Docs;
using ne14.services.docs.business.Features.Pdf;

/// <summary>
/// Pdf mq consumer.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PdfConsumer"/> class.
/// </remarks>
/// <param name="converter">The converter.</param>
public class PdfConsumer(IPdfConverter converter)
{
    /// <summary>
    /// Consumes the message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>Async task.</returns>
    public async Task Consume(PdfConversionRequired message)
    {
        await Task.CompletedTask;

        ////var str = await this.converter.FromUrl(url);
    }
}
