using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PropertyManager.Functions
{
    public static class MakePayment
    {
        [FunctionName("MakePayment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "propertyManager/makePayment/{arrangementId}/{ammount}")] 
            HttpRequest req, int arrangementId, double ammount, ILogger log)
        {
            log.LogInformation($"propertyManager/makePayment/{arrangementId}/{ammount} triggered.");

            if(!ArrangementService.IsValidArrangementId(arrangementId))
            {
                return new NotFoundObjectResult("'arrangementId' is not valid.");
            }

            if (ammount < 1)
            {
                return new NotFoundObjectResult("'ammount' is not valid.");
            }

            ArrangementService.MakePayment(arrangementId, ammount);

            return new OkObjectResult("Payment added.");
        }
    }
}
