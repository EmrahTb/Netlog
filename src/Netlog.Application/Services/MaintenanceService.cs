using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Netlog.Core.Entities;
using Netlog.Core.Interfaces;
using Netlog.Core.Repositories;
using Netlog.Application.Models;
using Netlog.Application.Mapper;
using Netlog.Application.Interfaces;

namespace Netlog.Application.Services
{
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _MaintenanceRepository;
        private readonly IAppLogger<MaintenanceService> _logger;

        public MaintenanceService(IMaintenanceRepository MaintenanceRepository, IAppLogger<MaintenanceService> logger)
        {
            _MaintenanceRepository = MaintenanceRepository ?? throw new ArgumentNullException(nameof(MaintenanceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<MaintenanceModel>> GetMaintenanceList()
        {
            var MaintenanceList = await _MaintenanceRepository.GetMaintenanceListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<MaintenanceModel>>(MaintenanceList);
            return mapped;
        }

        public async Task<MaintenanceModel> GetMaintenanceById(int MaintenanceId)
        {
            var Maintenance = await _MaintenanceRepository.GetByIdAsync(MaintenanceId);
            var mapped = ObjectMapper.Mapper.Map<MaintenanceModel>(Maintenance);
            return mapped;
        }
       

        public async Task<MaintenanceModel> Create(MaintenanceModel MaintenanceModel)
        {
            await ValidateMaintenanceIfExist(MaintenanceModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Maintenance>(MaintenanceModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            mappedEntity.CreateDate = DateTime.Now;
            mappedEntity.ModifyDate = DateTime.Now;
            mappedEntity.CreatedBy = 1;
            mappedEntity.ModifiedBy = 1;
            mappedEntity.IsDeleted = false;
            var newEntity = await _MaintenanceRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - NetlogAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<MaintenanceModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(MaintenanceModel MaintenanceModel)
        {
            ValidateMaintenanceIfNotExist(MaintenanceModel);
            
            var editMaintenance = await _MaintenanceRepository.GetByIdAsync(MaintenanceModel.ID);
            if (editMaintenance == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<MaintenanceModel, Maintenance>(MaintenanceModel, editMaintenance);
            editMaintenance.ModifyDate = DateTime.Now;
            editMaintenance.ModifiedBy = 1;
            await _MaintenanceRepository.UpdateAsync(editMaintenance);
            _logger.LogInformation($"Entity successfully updated - NetlogAppService");
        }

        public async Task Delete(MaintenanceModel MaintenanceModel)
        {
            ValidateMaintenanceIfNotExist(MaintenanceModel);
            var deletedMaintenance = await _MaintenanceRepository.GetByIdAsync(MaintenanceModel.ID);
            if (deletedMaintenance == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _MaintenanceRepository.DeleteAsync(deletedMaintenance);
            _logger.LogInformation($"Entity successfully deleted - NetlogAppService");
        }

        private async Task ValidateMaintenanceIfExist(MaintenanceModel MaintenanceModel)
        {
            var existingEntity = await _MaintenanceRepository.GetByIdAsync(MaintenanceModel.ID);
            if (existingEntity != null)
                throw new ApplicationException($"{MaintenanceModel.ToString()} with this id already exists");
        }

        private void ValidateMaintenanceIfNotExist(MaintenanceModel MaintenanceModel)
        {
            var existingEntity = _MaintenanceRepository.GetByIdAsync(MaintenanceModel.ID);
            if (existingEntity == null)
                throw new ApplicationException($"{MaintenanceModel.ToString()} with this id is not exists");
        }
        public async Task<IEnumerable<MaintenanceReport>> GetMaintenanceReport()
        {
            var MaintenanceList = await _MaintenanceRepository.GetMaintenanceReport();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<MaintenanceReport>>(MaintenanceList);
            return mapped;
        }

        public async Task<MaintenanceReport> GetMaintenanceReportById(int maintenanceId)
        {
            var MaintenanceList = await _MaintenanceRepository.GetMaintenanceReportById(maintenanceId);
            return MaintenanceList;
        }
    }
}
