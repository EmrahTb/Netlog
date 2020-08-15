using Netlog.Core.Entities;
using Netlog.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Core.Repositories
{
    public interface IMaintenanceRepository : IRepository<Maintenance>
    {
        Task<IEnumerable<Maintenance>> GetMaintenanceListAsync();
        Task<IEnumerable<Maintenance>> GetMaintenanceByNameAsync(string UserName);
        Task<IEnumerable<Maintenance>> GetMaintenanceByCategoryAsync(int categoryId);

        Task<IEnumerable<MaintenanceReport>> GetMaintenanceReport();
        Task<MaintenanceReport> GetMaintenanceReportById(int maintenanceId);

    }
}
