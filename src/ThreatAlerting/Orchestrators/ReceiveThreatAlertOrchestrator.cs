using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;
using ThreatAlertProcessing.Activities;

namespace ThreatAlertProcessing.Orchestrators
{
    public class ReceiveThreatAlertOrchestrator
    {

        [FunctionName(nameof(ReceiveThreatAlertOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var threatAlert = context.GetInput<ThreatAlert>();

            var saveResult= context.CallActivityAsync<SaveThreatAlertResult>(
                nameof(SaveThreatAlertActivity), 
                threatAlert);
        }
    }
}