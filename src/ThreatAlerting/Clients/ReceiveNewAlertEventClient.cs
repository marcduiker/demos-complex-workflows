using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;
using ThreatAlertProcessing.Orchestrators;

namespace ThreatAlertProcessing.Clients
{
    public class ReceiveNewAlertEventClient
    {
        private AbstractValidator<NewAlertEvent> _validator;
        public ReceiveNewAlertEventClient(AbstractValidator<NewAlertEvent> validator)
        {
            _validator = validator;
        }
        
        [FunctionName(nameof(ReceiveNewAlertEventClient))]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            [DurableClient] IDurableClient durableClient,
            ILogger log)
        {
            var threatAlert = await req.Content.ReadAsAsync<NewAlertEvent>();
            
            var validationResult = _validator.Validate(threatAlert);
            if (validationResult.IsValid)
            {
                var instanceId = await durableClient.StartNewAsync(
                    nameof(ReceiveNewEventAlertOrchestrator), 
                    threatAlert);
                
                return durableClient.CreateCheckStatusResponse(req, instanceId);
            }

            var conflictResponse = new HttpResponseMessage(HttpStatusCode.Conflict);
            conflictResponse.ReasonPhrase = validationResult.ToString();
            
            return conflictResponse;
        }
    }
}
