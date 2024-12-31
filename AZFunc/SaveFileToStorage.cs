using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AZFunc
{
    public class SaveFileToStorage
    {
        private readonly ILogger<SaveFileToStorage> _logger;

        public SaveFileToStorage(ILogger<SaveFileToStorage> logger)
        {
            _logger = logger;
        }

        [Function("SaveFileToStorage")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Started SaveFile-Worker...");

            if( !req.Headers.TryGetValue("file-type", out var fileTypeHandler))
            {
                return new BadRequestObjectResult("The 'file-type' header is mandatory");
            }

            var fileType = fileTypeHandler.ToString();
            var form = await req.ReadFormAsync();
            var file = form.Files["file"];

            if( file == null || file.Length == 0)
            {
                return new BadRequestObjectResult("No files were sent to Storage. Try sending a different file");
            }

            _logger.LogInformation("Starting Connection String...");

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = fileType;

            BlobClient blobClient = new BlobClient(connectionString, containerName, file.FileName);
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            

            string blobName = file.FileName;
            var blob = containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blob.UploadAsync(stream, true);
            }

            _logger.LogInformation($"File was sent successfully, name: {file.FileName}");

            return new OkObjectResult(new 
                {
                    Message="File was Saved Successfully!",
                    BlobUri=blob.Uri
                });
        }
    }
}
