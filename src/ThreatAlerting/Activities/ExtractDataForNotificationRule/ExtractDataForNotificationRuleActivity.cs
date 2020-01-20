using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;

namespace ThreatAlertProcessing.Activities.ExtractDataForNotificationRule
{
    public class ExtractDataForNotificationRuleActivity
    {
        [FunctionName(nameof(ExtractDataForNotificationRuleActivity))]
        public async Task<Tuple<NotificationRule, string>> Run(
            [ActivityTrigger] Tuple<NotificationRule, Alert> notificationRuleWithThreat,
            ILogger logger)
        {
            return new Tuple<NotificationRule, string>(notificationRuleWithThreat.Item1, string.Empty);
        }
    }
}