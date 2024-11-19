using Azure.Storage.Blobs;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var blobConnectionString = configuration.GetConnectionString("BlobConnectionString");
            var containerName = configuration["BlobContainer"];

            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            _containerClient.CreateIfNotExists();
        }

        public async Task<string> UploadFileAsync(FileUploadModel fileModel)
        {
            var file = fileModel.File;

            if (file == null || file.Length == 0)
                throw new ArgumentException("File is not provided or empty.");

            try
            {
                var fileName = !string.IsNullOrEmpty(fileModel.FileName) ? fileModel.FileName : file.FileName;
                var blobClient = _containerClient.GetBlobClient(fileName);

                using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, overwrite: true);

                var metadata = new Dictionary<string, string>
                    {
                        { "Category", fileModel.FileCategory ?? "Uncategorized" },
                        { "Description", fileModel.FileDescription ?? "No description" },
                        { "Name", fileName }
                    };
                await blobClient.SetMetadataAsync(metadata);

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to upload file: {ex.Message}", ex);
            }
        }       
    }
}
