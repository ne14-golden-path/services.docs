// <copyright file="IBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Blob;

/// <summary>
/// Blob repository.
/// </summary>
public interface IBlobRepository
{
    /// <summary>
    /// Uploads a new blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="blob">The blob.</param>
    /// <returns>The newly generated blob reference.</returns>
    public Task<Guid> UploadAsync(string containerName, BlobMeta blob);

    /// <summary>
    /// Downloads a blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>The blob.</returns>
    public Task<BlobMeta> DownloadAsync(string containerName, Guid blobReference);

    /// <summary>
    /// Deletes a blob.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="blobReference">The blob reference.</param>
    /// <returns>Async task.</returns>
    public Task DeleteAsync(string containerName, Guid blobReference);
}
