using Netlog.Core.Entities;

namespace Netlog.Infrastructure.Tests.Builders
{
    public class UserBuilder
    {
        private User _User;
        public int TestUserId => 35;
        public string TestFirstName => "Test User Names";
        public string TestLastName => "Test Last Names";

        public UserBuilder()
        {
            _User = WithDefaultValues();
        }

        public User Build()
        {
            return _User;
        }

        public User WithDefaultValues()
        {
            return User.Create(TestUserId, TestFirstName, TestLastName);
        }

        public User WithAllValues()
        {
            return User.Create(TestUserId, TestFirstName, TestLastName);
        }
    }
}
