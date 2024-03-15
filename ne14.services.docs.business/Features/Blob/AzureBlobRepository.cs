// <copyright file="AzureBlobRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.business.Features.Blob;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ne14.library.fluent_errors.Extensions;

/// <inheritdoc cref="IBlobRepository"/>
public class AzureBlobRepository(BlobServiceClient blobService) : IBlobRepository
{
    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(string containerName, string fileName, Stream content)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        if (!await container.ExistsAsync())
        {
            await container.CreateAsync();
        }

        var blobReference = Guid.NewGuid();
        var metadata = new Dictionary<string, string> { ["filename"] = fileName };
        var blob = container.GetBlobClient(blobReference.ToString());
        var uploadResult = await blob.UploadAsync(content, new BlobUploadOptions { Metadata = metadata });
        uploadResult.GetRawResponse().IsError.MustBe(false);

        return blobReference;
    }

    /// <inheritdoc/>
    public async Task<Stream> DownloadAsync(string containerName, Guid blobReference)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        var blob = container.GetBlobClient(blobReference.ToString());
        var retVal = new MemoryStream();
        var result = await blob.DownloadToAsync(retVal);
        result.IsError.MustBe(false);

        var metadata = (await blob.GetPropertiesAsync()).Value.Metadata;
        return retVal;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(string containerName, Guid blobReference)
    {
        var container = blobService.GetBlobContainerClient(containerName);
        var blob = container.GetBlobClient(blobReference.ToString());
        var result = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        result.GetRawResponse().IsError.MustBe(false);
    }
}
