using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RememberUtility.Enum;
using RememberUtility.Model;

namespace RememberUtility.Interface
{
    public interface IUser
    {
        void AddUser(UserLogin userLogin);

        UserLogin CheckUser(string username, string password);

        UserLogin CheckUser(string username);

        bool UpdateUser(string currentUserName, string newUserName, string newPassword);

        bool DeleteUser(string username);

        List<UserLogin> GetListUsers();

        void SaveUserDb();

        void ReloadDatbase();
    }
}
