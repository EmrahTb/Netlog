using System;

namespace Netlog.Core.Entities
{
    public class MaintenanceReport 
    {
        public int ID { get; set; }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public string Status { get; set; }
    }
}
