using Netlog.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Interfaces
{
    public interface IUserPageService
    {
        Task<IEnumerable<UserModel>> GetUsers(string UserName);
        Task<UserModel> GetUserById(int UserId);
        Task<IEnumerable<UserModel>> GetUserByCategory(int categoryId);
        Task<UserModel> CreateUser(UserModel UserModel);
        Task UpdateUser(UserModel UserModel);
        Task DeleteUser(UserModel UserModel);
    }
}
