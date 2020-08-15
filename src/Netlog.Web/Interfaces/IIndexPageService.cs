using Netlog.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netlog.Web.Interfaces
{
    // NOTE : This is the whole page service, it could be include all categories and Users
    // this is the razor page based service
    public interface IIndexPageService
    {
        Task<IEnumerable<UserModel>> GetUsers();        
    }
}
