using Netlog.Core.Entities;
using Netlog.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUserListAsync();
        Task<IEnumerable<User>> GetUserByNameAsync(string UserName);
        Task<IEnumerable<User>> GetUserByCategoryAsync(int categoryId);
    }
}
