using ThreatAlerting.Models;

namespace ThreatAlertProcessing.Activities.ExtractDataForNotificationRule
{
    public class NotificationRuleWithThreatData
    {
        public NotificationRule NotificationRule { get; set; }

        public string AlertData { get; set; }
    }
}