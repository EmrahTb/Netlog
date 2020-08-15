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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(NetlogContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetUserListAsync()
        {
            return await _dbContext.Users
                           .ToListAsync();

            // second way
            // return await GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string UserName)
        {
            return await _dbContext.Users
                     .Where(x => x.FirstName == UserName)
                     .ToListAsync();

            // second way
            // return await GetAsync(x => x.UserName.ToLower().Contains(UserName.ToLower()));

            // third way
            //return await _dbContext.Users
            //    .Where(x => x.UserName.Contains(UserName))
            //    .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserByCategoryAsync(int userId)
        {
            return await _dbContext.Users
                .Where(x => x.ID== userId)
                .ToListAsync();
        }
    }
}
