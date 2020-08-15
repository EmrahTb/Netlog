using Netlog.Core.Entities;
using Xunit;

namespace Netlog.Core.Tests.Entities
{
    public class UserTests
    {
        private int _testProductId = 1;
        private string _testFirstName = "Emrah";
        private string _testLastName = "Temurbalık";

        [Fact]
        public void Create_Product()
        {
            var product = User.Create(_testProductId,_testFirstName,_testLastName);

            Assert.Equal(_testProductId, product.ID);
            Assert.Equal(_testFirstName, product.FirstName);
            Assert.Equal(_testLastName, product.LastName);
        }
    }
}
