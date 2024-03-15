// <copyright file="BlobMeta.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Blob;

/// <summary>
/// Blob meta.
/// </summary>
/// <param name="Content">The content stream.</param>
/// <param name="FileName">The file name.</param>
public record BlobMeta(Stream Content, string FileName);
