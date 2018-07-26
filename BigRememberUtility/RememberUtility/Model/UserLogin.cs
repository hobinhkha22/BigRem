using ConnectionSampleCode.Enum;

namespace ConnectionSampleCode.Model
{
    public class UserLogin
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordEncrypt { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }
        public UserRoleEnum UserRole { get; set; }
    }
}