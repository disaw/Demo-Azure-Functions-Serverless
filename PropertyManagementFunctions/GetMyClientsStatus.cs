using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PropertyManagementFunctions.Models;
using System.Collections.Generic;

namespace PropertyManagementFunctions
{
    public static class GetMyClientsStatus
    {
        [FunctionName("GetMyClientsStatus")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "propertyManager/{managerId}/getMyClientsStatus")]
        HttpRequest req, int managerId, ILogger log)
        {
            log.LogInformation($"propertyManager/{managerId}/getMyClientsStatus triggered.");

            if (managerId < 1)
            {
                return new NotFoundObjectResult("'managerId' is not valid.");
            }

            var statuses = new List<ClientStatus>();
            statuses.Add(
               new ClientStatus
               {
                   Client = new Client { Id = 16, Name = "Nad", ArrangementId = 11, PropertyId = 45 },
                   Status = "GREEN"
               });
            statuses.Add(
                new ClientStatus { Client = new Client { Id = 2, Name = "Sam", ArrangementId = 4, PropertyId = 20 },
                Status = "RED"
                });
            statuses.Add(
                new ClientStatus
                {
                    Client = new Client { Id = 8, Name = "Dave", ArrangementId = 7, PropertyId = 31 },
                    Status = "RED"
                });
            statuses.Add(
                new ClientStatus
                {
                    Client = new Client { Id = 46, Name = "Steve", ArrangementId = 22, PropertyId = 78 },
                    Status = "AMBER"
                });

            return new OkObjectResult(statuses);
        }
    }
}
