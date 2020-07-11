using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PropertyManager.Functions
{
    public static class PeekDataBase
    {
        [FunctionName("PeekDataBase")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "propertyManager/peekDataBase/{reset}")] 
            HttpRequest req, bool reset, ILogger log)
        {
            log.LogInformation($"propertyManager/peekDataBase triggered.");

            return new OkObjectResult(ArrangementService.PeekDataBase(reset));
        }
    }
}
