using Netlog.Core.Entities;
using Netlog.Core.Repositories;
using Netlog.Core.Specifications;
using Netlog.Infrastructure.Data;
using Netlog.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlog.Infrastructure.Repository
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(NetlogContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenanceListAsync()
        {
            return await _dbContext.Maintenances
                    .ToListAsync();
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenanceByNameAsync(string MaintenanceName)
        {
            return await _dbContext.Maintenances
                     .Where(x => x.Description == MaintenanceName)
                     .ToListAsync();
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenanceByCategoryAsync(int MaintenanceId)
        {
            return await _dbContext.Maintenances
                .Where(x => x.ID == MaintenanceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MaintenanceReport>> GetMaintenanceReport()
        {
            var data = from m in _dbContext.Maintenances
                       join v in _dbContext.Vehicles on m.VehicleID equals v.ID
                       join u in _dbContext.Users on m.UserID equals u.ID
                       join p in _dbContext.PictureGroups on m.PictureGroupID equals p.ID
                       join s in _dbContext.Status on m.StatusID equals s.ID
                       join vt in _dbContext.VehicleTypes on v.VehicleTypeID equals vt.ID
                       select new MaintenanceReport
                       {
                           ID = m.ID,
                           Description = m.Description,
                           ExpectedTimeToFix = m.ExpectedTimeToFix,
                           LocationLatitude = m.LocationLatitude,
                           LocationLongitude = m.LocationLongitude,
                           Picture = p.PictureImage,
                           Status = s.Name,
                           UserName = (u.FirstName + " " + u.LastName),
                           VehicleName = v.PlateNo,
                           VehicleType = vt.Name
                       };
            return await data.ToListAsync();
        }

        public Task<MaintenanceReport> GetMaintenanceReportById(int maintenanceId)
        {
            var data = (from m in _dbContext.Maintenances
                       join v in _dbContext.Vehicles on m.VehicleID equals v.ID
                       join u in _dbContext.Users on m.UserID equals u.ID
                       join p in _dbContext.PictureGroups on m.PictureGroupID equals p.ID
                       join s in _dbContext.Status on m.StatusID equals s.ID
                       join vt in _dbContext.VehicleTypes on v.VehicleTypeID equals vt.ID
                       where m.ID == maintenanceId
                       select new MaintenanceReport
                       {
                           ID = m.ID,
                           Description = m.Description,
                           ExpectedTimeToFix = m.ExpectedTimeToFix,
                           LocationLatitude = m.LocationLatitude,
                           LocationLongitude = m.LocationLongitude,
                           Picture = p.PictureImage,
                           Status = s.Name,
                           UserName = (u.FirstName + " " + u.LastName),
                           VehicleName = v.PlateNo,
                           VehicleType = vt.Name
                       }).FirstOrDefaultAsync();

            return data;
        }
    }
}
