using Netlog.Application.Interfaces;
using Netlog.Application.Models;
using Netlog.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/maintenance")]
    public class MaintenanceController : ControllerBase
    {
        private IMaintenanceService _MaintenanceService;
        public MaintenanceController(IMaintenanceService MaintenanceService)
        {
            _MaintenanceService = MaintenanceService;
        }

        //[Authorize]
        //[HttpGet("MaintenanceList")]
        //public IActionResult GetAll()
        //{
        //    var ApiMaintenances = _MaintenanceService.GetMaintenanceList();
        //    return Ok(ApiMaintenances);
        //}

        [Authorize]
        [HttpPost("maintenanceList")]
        public async Task<IEnumerable<MaintenanceModel>> GetMaintenances()
        {
            var list = await _MaintenanceService.GetMaintenanceList();
            return list;
        }

        [Authorize]
        [HttpPost("maintenanceReport")]
        public async Task<IEnumerable<MaintenanceReport>> GetMaintenanceReport()
        {
            var list = await _MaintenanceService.GetMaintenanceReport();
            return list;
        }

        [Authorize]
        [HttpPost("reportDetail")]
        public async Task<MaintenanceReport> GetMaintenanceReportById(int maintenanceId)
        {
            var list = await _MaintenanceService.GetMaintenanceReportById(maintenanceId);
            return list;
        }



        [Authorize]
        [HttpPost("addMaintenance")]
        public async Task<string> AddMaintenance(MaintenanceModel Maintenance)
        {
            try
            {
                var list = await _MaintenanceService.Create(Maintenance);
                return "Success";
            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        [Authorize]
        [HttpPost("updateMaintenance")]
        public async Task<string> UpdateMaintenance(MaintenanceModel Maintenance)
        {
            try
            {
                await _MaintenanceService.Update(Maintenance);
                return "Success";

            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }


        [Authorize]
        [HttpPost("deleteMaintenance")]
        public async Task<string> DeleteMaintenance(MaintenanceModel Maintenance)
        {
            try
            {
                await _MaintenanceService.Delete(Maintenance);
                return "Success";

            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }
    }
}
