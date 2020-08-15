using Netlog.Core.Entities.Base;
using System;

namespace Netlog.Core.Entities
{
    public class Vehicle : Entity
    {
        public Vehicle()
        {
        }

        public string PlateNo { get; set; }
        public int VehicleTypeID { get; set; }
        public string UserID { get; set; }
    }
}
