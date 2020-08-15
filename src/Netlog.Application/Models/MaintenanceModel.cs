using Netlog.Application.Models.Base;
using System;

namespace Netlog.Application.Models
{
    public class MaintenanceModel : BaseModel
    {
        public int VehicleID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public int PictureGroupID { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public int ResponsibleUserID { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public int StatusID { get; set; }
    }
}
