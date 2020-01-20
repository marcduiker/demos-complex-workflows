using System;
using System.Security.Cryptography.X509Certificates;

namespace ThreatAlerting.Models
{
    public class NotificationRule
    {
        public Guid Company { get; set; }

        public Guid RuleId { get; set; }

        public string Description { get; set; }

        public string RuleJsonPath { get; set; }
    }
}