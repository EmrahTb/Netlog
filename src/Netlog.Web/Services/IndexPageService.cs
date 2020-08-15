using Netlog.Application.Interfaces;
using Netlog.Application.Models;
using Netlog.Web.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Services
{
    public class IndexPageService : IIndexPageService
    {
        private readonly IUserService _UserAppService;        
        private readonly IMapper _mapper;

        public IndexPageService(IUserService UserAppService, IMapper mapper)
        {
            _UserAppService = UserAppService ?? throw new ArgumentNullException(nameof(UserAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var list = await _UserAppService.GetUserList();
            var mapped = _mapper.Map<IEnumerable<UserModel>>(list);
            return mapped;
        }
    }
}
