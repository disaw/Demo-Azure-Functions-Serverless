using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using PropertyManagementFunctions.Models;

namespace PropertyManagementFunctions
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

            if(managerId < 1)
            {
                return new NotFoundObjectResult("'managerId' is not valid.");
            }

            var clients = new List<Client>();
            clients.Add(new Client { Id = 2, Name = "Sam", ArrangementId = 4, PropertyId = 20 });
            clients.Add(new Client { Id = 8, Name = "Dave", ArrangementId = 7, PropertyId = 31 });

            return new OkObjectResult(clients);
        }
    }
}
