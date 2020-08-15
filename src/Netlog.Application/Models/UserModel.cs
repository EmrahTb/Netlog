using Netlog.Application.Models.Base;

namespace Netlog.Application.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string profilepicture { get; set; }
    }
}
