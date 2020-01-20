using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreatAlerting.Models
{
    public class NewAlertEvent
    {
        public Guid Id { get; set; }
        
        public AlertType AlertType { get; set; }
        
        public DateTime EventReceivedDateTime { get; set; }
        
        public DateTime AlertDetectedDateTime { get; set; }

        public Guid CompanyId { get; set; }
        
        public Dictionary<string, object> Attributes { get; set; }
        
        [IgnoreDataMember]
        public Guid GetAlertId => Attributes.TryGetValue("alert_id", out var threatId) ? (Guid)threatId : Guid.Empty;
        
        [IgnoreDataMember]
        public Guid GetVesselId => Attributes.TryGetValue("vessel_id", out var vesselId) ? (Guid)vesselId : Guid.Empty;
    }
}