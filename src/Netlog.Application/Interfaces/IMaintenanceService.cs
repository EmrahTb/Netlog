using Netlog.Application.Models;
using Netlog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Application.Interfaces
{
    public interface IMaintenanceService
    {
        Task<IEnumerable<MaintenanceModel>> GetMaintenanceList();
        Task<MaintenanceModel> GetMaintenanceById(int MaintenanceId);
        Task<IEnumerable<MaintenanceReport>> GetMaintenanceReport();
        Task<MaintenanceReport> GetMaintenanceReportById(int maintenanceId);
        Task<MaintenanceModel> Create(MaintenanceModel MaintenanceModel);
        Task Update(MaintenanceModel MaintenanceModel);
        Task Delete(MaintenanceModel MaintenanceModel);
    }
}
