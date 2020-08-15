using Netlog.Application.Interfaces;
using Netlog.Application.Models;
using Netlog.Core.Entities;
using Netlog.Web.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Services
{
    public class MaintenancePageService : IMaintenancePageService
    {
        private readonly IMaintenanceService _MaintenanceAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<MaintenancePageService> _logger;

        public MaintenancePageService(IMaintenanceService MaintenanceAppService, IMapper mapper, ILogger<MaintenancePageService> logger)
        {
            _MaintenanceAppService = MaintenanceAppService ?? throw new ArgumentNullException(nameof(MaintenanceAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<MaintenanceReport>> GetMaintenances()
        {
            var list = await _MaintenanceAppService.GetMaintenanceReport();
            var mappedByName = _mapper.Map<IEnumerable<MaintenanceReport>>(list);
            return mappedByName;
        }

        public async Task<MaintenanceReport> GetMaintenanceDetail(int MaintenanceId)
        {
            var Maintenance = await _MaintenanceAppService.GetMaintenanceReportById(MaintenanceId);
            var mapped = _mapper.Map<MaintenanceReport>(Maintenance);
            return mapped;
        }
    }
}
