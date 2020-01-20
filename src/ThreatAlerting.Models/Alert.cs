using System;

namespace ThreatAlerting.Models
{
    public class Alert
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Remediation { get; set; }
    }
}