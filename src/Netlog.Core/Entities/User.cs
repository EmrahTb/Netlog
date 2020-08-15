using Netlog.Core.Entities.Base;

namespace Netlog.Core.Entities
{
    public class User : Entity
    {
        public User()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string profilepicture { get; set; }


        public static User Create(int userId,string firstName, string lastName)
        {
            var User = new User
            {
                ID = userId,
                FirstName = firstName,
                LastName = lastName
            };
            return User;
        }
    }
}
