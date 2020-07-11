using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PropertyManager.Functions
{
    public static class GetArrearsClients
    {
        [FunctionName("GetArrearsClients")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "propertyManager/{managerId}/getArrearsClients")] 
            HttpRequest req, int managerId, ILogger log)
        {
            log.LogInformation($"propertyManager/{managerId}/getArrearsClients triggered.");

            if(!ArrangementService.IsValidManagerId(managerId))
            {
                return new NotFoundObjectResult("'managerId' is not valid.");
            }

            var clients = ArrangementService.GetArrearsClients(managerId);

            return new OkObjectResult(clients);
        }
    }
}
