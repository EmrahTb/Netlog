using Netlog.Infrastructure.Data;
using Netlog.Infrastructure.Repository;
using Netlog.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Netlog.Infrastructure.Tests.Repositories
{
    public class UserTests
    {
        private readonly NetlogContext _NetlogContext;
        private readonly UserRepository _userRepository;
        private readonly ITestOutputHelper _output;
        private UserBuilder UserBuilder { get; } = new UserBuilder();

        public UserTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<NetlogContext>()
                .UseInMemoryDatabase(databaseName: "Netlog")
                .Options;
            _NetlogContext = new NetlogContext(dbOptions);
            _userRepository = new UserRepository(_NetlogContext);
        }

        [Fact]
        public async Task Get_User_By_Name()
        {
            var existingUser = UserBuilder.WithDefaultValues();
            _NetlogContext.Users.Add(existingUser);

            _NetlogContext.SaveChanges();
            var FirstName = existingUser.FirstName;
            _output.WriteLine($"FirstName: {FirstName}");

            var UserListFromRepo = await _userRepository.GetUserByNameAsync(FirstName);
            Assert.Equal(UserBuilder.TestFirstName, UserListFromRepo.ToList().First().FirstName);
        }
    }
}
