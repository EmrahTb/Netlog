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
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IAppLogger<UserService> _logger;

        public UserService(IUserRepository UserRepository, IAppLogger<UserService> logger)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UserModel>> GetUserList()
        {
            var UserList = await _UserRepository.GetUserListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserModel>>(UserList);
            return mapped;
        }

        public async Task<UserModel> GetUserById(int UserId)
        {
            var User = await _UserRepository.GetByIdAsync(UserId);
            var mapped = ObjectMapper.Mapper.Map<UserModel>(User);
            return mapped;
        }

        public async Task<IEnumerable<UserModel>> GetUserByName(string UserName)
        {
            var UserList = await _UserRepository.GetUserByNameAsync(UserName);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserModel>>(UserList);
            return mapped;
        }

        public async Task<IEnumerable<UserModel>> GetUserByCategory(int categoryId)
        {
            var UserList = await _UserRepository.GetUserByCategoryAsync(categoryId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserModel>>(UserList);
            return mapped;
        }

        public async Task<UserModel> Create(UserModel UserModel)
        {
            await ValidateUserIfExist(UserModel);

            var mappedEntity = ObjectMapper.Mapper.Map<User>(UserModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            mappedEntity.CreateDate = DateTime.Now;
            mappedEntity.ModifyDate = DateTime.Now;
            mappedEntity.CreatedBy = 1;
            mappedEntity.ModifiedBy = 1;
            mappedEntity.IsDeleted = false;
            var newEntity = await _UserRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - NetlogAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<UserModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(UserModel UserModel)
        {
            ValidateUserIfNotExist(UserModel);
            
            var editUser = await _UserRepository.GetByIdAsync(UserModel.ID);
            if (editUser == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<UserModel, User>(UserModel, editUser);
            editUser.ModifyDate = DateTime.Now;
            editUser.ModifiedBy = 1;
            await _UserRepository.UpdateAsync(editUser);
            _logger.LogInformation($"Entity successfully updated - NetlogAppService");
        }

        public async Task Delete(UserModel UserModel)
        {
            ValidateUserIfNotExist(UserModel);
            var deletedUser = await _UserRepository.GetByIdAsync(UserModel.ID);
            if (deletedUser == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _UserRepository.DeleteAsync(deletedUser);
            _logger.LogInformation($"Entity successfully deleted - NetlogAppService");
        }

        private async Task ValidateUserIfExist(UserModel UserModel)
        {
            var existingEntity = await _UserRepository.GetByIdAsync(UserModel.ID);
            if (existingEntity != null)
                throw new ApplicationException($"{UserModel.ToString()} with this id already exists");
        }

        private void ValidateUserIfNotExist(UserModel UserModel)
        {
            var existingEntity = _UserRepository.GetByIdAsync(UserModel.ID);
            if (existingEntity == null)
                throw new ApplicationException($"{UserModel.ToString()} with this id is not exists");
        }
    }
}
