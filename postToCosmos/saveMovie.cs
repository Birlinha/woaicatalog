using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace postToCosmos
{
    public class saveMovie
    {
        private readonly ILogger<saveMovie> _logger;

        public saveMovie(ILogger<saveMovie> logger)
        {
            _logger = logger;
        }

        [Function("saveMovie")]
        [CosmosDBOutput("%DatabaseName%", "movies", Connection = "CosmoDBConnection", CreateIfNotExists = true, PartitionKey = "id")]
        public async Task<object?> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            MovieRequest movie = null;

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                movie = JsonConvert.DeserializeObject<MovieRequest>(content);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error while json deserializing object: " + ex.Message);
            }

            return JsonConvert.SerializeObject(movie);
        }
    }
}
