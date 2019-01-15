using System.ComponentModel;
using RememberUtility.Enum;

namespace RememberUtility.Model
{
    public class UserLogin
    {
        [DisplayName("User id")]
        public string UserId { get; set; }
        [DisplayName("User name")]
        public string Username { get; set; }
        [DisplayName("Password encrypt")]
        public string PasswordEncrypt { get; set; }
        [DisplayName("Created date")]
        public string CreatedDate { get; set; }
        [DisplayName("Last mod date")]
        public string LastModifiedDate { get; set; }
        [DisplayName("User Role")]
        public UserRoleEnum UserRole { get; set; }
    }
}