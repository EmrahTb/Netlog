using Netlog.Application.Models.Base;
using System;

namespace Netlog.Application.Models
{
    public class MaintenanceReportModel : BaseModel
    {
        public int VehicleName { get; set; }
        public int UserName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public string Status { get; set; }
    }
}
