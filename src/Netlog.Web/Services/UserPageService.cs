using Netlog.Application.Interfaces;
using Netlog.Application.Models;
using Netlog.Web.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Services
{
    public class UserPageService : IUserPageService
    {
        private readonly IUserService _UserAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserPageService> _logger;

        public UserPageService(IUserService UserAppService, IMapper mapper, ILogger<UserPageService> logger)
        {
            _UserAppService = UserAppService ?? throw new ArgumentNullException(nameof(UserAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UserModel>> GetUsers(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                var list = await _UserAppService.GetUserList();
                var mapped = _mapper.Map<IEnumerable<UserModel>>(list);
                return mapped;
            }

            var listByName = await _UserAppService.GetUserByName(UserName);
            var mappedByName = _mapper.Map<IEnumerable<UserModel>>(listByName);
            return mappedByName;
        }

        public async Task<UserModel> GetUserById(int UserId)
        {
            var User = await _UserAppService.GetUserById(UserId);
            var mapped = _mapper.Map<UserModel>(User);
            return mapped;
        }

        public async Task<IEnumerable<UserModel>> GetUserByCategory(int categoryId)
        {
            var list = await _UserAppService.GetUserByCategory(categoryId);
            var mapped = _mapper.Map<IEnumerable<UserModel>>(list);
            return mapped;
        }
        public async Task<UserModel> CreateUser(UserModel UserViewModel)
        {
            var mapped = _mapper.Map<UserModel>(UserViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await _UserAppService.Create(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");

            var mappedViewModel = _mapper.Map<UserModel>(entityDto);
            return mappedViewModel;
        }

        public async Task UpdateUser(UserModel UserViewModel)
        {
            var mapped = _mapper.Map<UserModel>(UserViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await _UserAppService.Update(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");
        }

        public async Task DeleteUser(UserModel UserViewModel)
        {
            var mapped = _mapper.Map<UserModel>(UserViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            await _UserAppService.Delete(mapped);
            _logger.LogInformation($"Entity successfully added - IndexPageService");
        }
    }
}
