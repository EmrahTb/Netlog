using Netlog.Application.Interfaces;
using Netlog.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpPost("userList")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var list = await _userService.GetUserList();
            return list;
        }

        [Authorize]
        [HttpPost("addUSer")]
        public async Task<string> AddUser(UserModel user)
        {
            try
            {
                var list = await _userService.Create(user);
                return "Success";
            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        [Authorize]
        [HttpPost("updateUser")]
        public async Task<string> UpdateUser(UserModel user)
        {
            try
            {
                await _userService.Update(user);
                return "Success";

            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }


        [Authorize]
        [HttpPost("deleteUser")]
        public async Task<string> DeleteUser(UserModel user)
        {
            try
            {
                await _userService.Delete(user);
                return "Success";

            }
            catch (System.Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }
    }
}
