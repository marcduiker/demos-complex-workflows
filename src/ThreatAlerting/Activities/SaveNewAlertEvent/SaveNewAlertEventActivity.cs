using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;

namespace ThreatAlertProcessing.Activities.SaveNewAlertEvent
{
    public class SaveNewAlertEventActivity
    {
        [FunctionName(nameof(SaveNewAlertEventActivity))]
        public async Task<SaveThreatAlertResult> Run(
            [ActivityTrigger] NewAlertEvent newAlertEvent,
            ILogger logger)
        {
            return new SaveThreatAlertResult();
        }
    }
}