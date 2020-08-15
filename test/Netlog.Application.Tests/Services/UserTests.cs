using Netlog.Application.Exceptions;
using Netlog.Application.Services;
using Netlog.Core.Entities;
using Netlog.Core.Interfaces;
using Netlog.Core.Repositories;
using Netlog.Core.Repositories.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Netlog.Application.Tests.Services
{
    public class UserTests
    {
        // NOTE : This layer we are not loaded database objects, test functionality of application layer

        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAppLogger<UserService>> _mockAppLogger;

        public UserTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAppLogger = new Mock<IAppLogger<UserService>>();
        }      

        [Fact]
        public async Task Get_User_List()
        {
            var User1 = User.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());
            var User2 = User.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());
            _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(User1);
            _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(User2);

            var UserService = new UserService(_mockUserRepository.Object, _mockAppLogger.Object);
            var UserList = await UserService.GetUserList();

            _mockUserRepository.Verify(x => x.GetUserListAsync(), Times.Once);
        }
    }
}
