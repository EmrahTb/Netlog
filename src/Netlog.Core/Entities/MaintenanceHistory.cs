using Netlog.Core.Entities.Base;
using System;

namespace Netlog.Core.Entities
{
    public class MaintenanceHistory : Entity
    {
        public MaintenanceHistory()
        {
        }

        public int MaintenanceID { get; set; }
        public int ActionTypeID { get; set; }
    }
}
