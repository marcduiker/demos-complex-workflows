using System;

namespace ThreatAlerting.Models
{
    public class ThreatAlert
    {
        public Guid Id { get; set; }

        public AlertType AlertType { get; set; }

        public Guid ThreatId { get; set; }
    }
}