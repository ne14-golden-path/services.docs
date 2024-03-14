// <copyright file="AzureBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Blob;

using Azure.Storage.Blobs;

/// <inheritdoc cref="IBlobRepository"/>
public class AzureBlobRepository(BlobServiceClient blobService) : IBlobRepository
{
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(string containerName, string fileName, Stream content)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();

        var blobReference = Guid.NewGuid();
        var blob = container.GetBlobClient(blobReference.ToString());
        await blob.UploadAsync(content);
        await blob.SetMetadataAsync(new Dictionary<string, string> { ["filename"] = fileName });

        return blobReference;
    }

    /// <inheritdoc/>
    public async Task<Stream> DownloadAsync(string containerName, Guid blobReference)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        var blob = container.GetBlobClient(blobReference.ToString());
        var result = await blob.DownloadContentAsync();
        var fileName = result.Value.Details.Metadata["filename"];
        return result.Value.Content.ToStream();
    }
}
