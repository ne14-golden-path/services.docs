// <copyright file="IPdfConverter.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Pdf;

/// <summary>
/// Converts pdf files.
/// </summary>
public interface IPdfConverter
{
    /// <summary>
    /// Converts a website to a pdf document.
    /// </summary>
    /// <param name="url">The website url.</param>
    /// <returns>A pdf document.</returns>
    public Task<Stream> FromUrl(string url);

    /// <summary>
    /// Converts html to a pdf document.
    /// </summary>
    /// <param name="htmlContent">The html content.</param>
    /// <returns>A pdf document.</returns>
    public Task<Stream> FromHtml(Stream htmlContent);
}
