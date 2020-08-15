using Netlog.Application.Models;
using Netlog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Interfaces
{
    public interface IMaintenancePageService
    {
        Task<IEnumerable<MaintenanceReport>> GetMaintenances();
        Task<MaintenanceReport> GetMaintenanceDetail(int MaintenanceId);
    }
}
