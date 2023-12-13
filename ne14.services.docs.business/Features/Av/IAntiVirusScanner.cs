// <copyright file="IAntiVirusScanner.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Av;

/// <summary>
/// Anti-virus scanner.
/// </summary>
public interface IAntiVirusScanner
{
    /// <summary>
    /// Scans a stream of bytes.
    /// </summary>
    /// <param name="content">The contents.</param>
    /// <returns>Async task.</returns>
    public Task AssertIsClean(Stream content);
}
