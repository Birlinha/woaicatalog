using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Started SaveFile-Worker...");

            _logger.LogInformation("Finishing SaveFile-Worker...");

            return new OkObjectResult(new {Message="Welcome to Azure Functions!",BlobUri="this/is/your/saved_file.file"});
        }
    }
}
