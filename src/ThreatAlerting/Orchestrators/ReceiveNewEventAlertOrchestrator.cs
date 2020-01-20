using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using ThreatAlerting.Models;
using ThreatAlertProcessing.Activities;
using ThreatAlertProcessing.Activities.ExtractDataForNotificationRule;
using ThreatAlertProcessing.Activities.GetNotificationRules;
using ThreatAlertProcessing.Activities.SaveNewAlertEvent;

namespace ThreatAlertProcessing.Orchestrators
{
    public class ReceiveNewEventAlertOrchestrator
    {
        [FunctionName(nameof(ReceiveNewEventAlertOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var threatAlert = context.GetInput<NewAlertEvent>();

            var saveResult= await context.CallActivityAsync<SaveThreatAlertResult>(
                nameof(SaveNewAlertEventActivity), 
                threatAlert);

            var notificationRulesForCompany = await context.CallActivityAsync<IEnumerable<NotificationRule>>(
                nameof(GetNotificationRulesActivity),
                threatAlert.CompanyId);

            if (threatAlert.AlertType == AlertType.Single)
            {
                var extractRuleDataTasks = new List<Task>();
                
                // TODO GetFullThreatData
                var threatData = new Alert();

                foreach (var notificationRule in notificationRulesForCompany)
                {
                    var input = new Tuple<NotificationRule, Alert>(notificationRule, threatData);
                    extractRuleDataTasks.Add(context.CallActivityAsync<Tuple<NotificationRule, string>>(
                        nameof(ExtractDataForNotificationRuleActivity),
                        input));
                }

                await Task.WhenAll(extractRuleDataTasks);

                

            }
        }
    }
}