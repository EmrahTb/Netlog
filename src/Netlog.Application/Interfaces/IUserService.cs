using Netlog.Application.Models;
using Netlog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUserList();
        Task<UserModel> GetUserById(int UserId);
        Task<IEnumerable<UserModel>> GetUserByName(string UserName);
        Task<IEnumerable<UserModel>> GetUserByCategory(int categoryId);
        Task<UserModel> Create(UserModel UserModel);
        Task Update(UserModel UserModel);
        Task Delete(UserModel UserModel);
    }
}
