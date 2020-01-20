using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;
using ThreatAlertProcessing.Orchestrators;

namespace ThreatAlertProcessing.Clients
{
    public class ReceiveThreatAlertClient
    {
        [FunctionName(nameof(ReceiveThreatAlertClient))]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            [DurableClient] IDurableClient durableClient,
            ILogger log)
        {
            var threatAlert = await req.Content.ReadAsAsync<ThreatAlert>();
            var instanceId = await durableClient.StartNewAsync(
                nameof(ReceiveThreatAlertOrchestrator), 
                threatAlert);

            return durableClient.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
