using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.Interface
{
    public interface IUser
    {
        void AddUser(UserLogin userLogin);

        UserLogin CheckUser(string username, string password);

        bool UpdateUser(string currentUserName, string newUserName, string newPassword);

        bool DeleteUser(string username);

        List<UserLogin> GetListUsers();
    }
}
