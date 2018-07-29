using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;

namespace ConnectionSampleCode.HandleUtil
{
    public class UserUtil : IUser
    {
        private readonly FileHandlerUtil _fileHandlerUtil;

        public UserUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
        }

        public void AddUser(UserLogin userLogin)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.USERLOGIN);

            userLogin.CreatedDate = $"{DateTime.Now:MMMM dd,yyyy}";
            userLogin.UserId = HandleRandom.RandomString(10);
            HandleRandom.Encrypt(userLogin.PasswordEncrypt);
            userLogin.UserRole = UserRoleEnum.NormalUser;

            _fileHandlerUtil.JsonModel.UserLogin.Add(userLogin);

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
        }

        public UserLogin CheckUser(string username, string password)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.USERLOGIN);

            var findUser = _fileHandlerUtil.JsonModel.UserLogin.
                Find(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) &&
                          string.Equals(HandleRandom.Decrypt(x.PasswordEncrypt), password, StringComparison.CurrentCultureIgnoreCase));

            if (findUser != null)
            {
                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);

                return findUser;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);

            return null;
        }

        public bool UpdateUser(string currentUserName, string newUserName, string newPassword)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.USERLOGIN);

            var getCurrentUser = _fileHandlerUtil.JsonModel.UserLogin.
                Find(x => string.Equals(x.Username, currentUserName, StringComparison.CurrentCultureIgnoreCase));
            var indexOfUser = _fileHandlerUtil.JsonModel.UserLogin.IndexOf(getCurrentUser);

            if (getCurrentUser != null)
            {
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].Username = newUserName;
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].PasswordEncrypt = HandleRandom.Encrypt(newPassword); // encrypt and save
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";
                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
            return false;
        }

        public bool DeleteUser(string username)
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.USERLOGIN);
            if (username == null) return false;

            var getUser = _fileHandlerUtil.JsonModel.UserLogin.Find(x =>
                string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));

            if (getUser != null)
            {
                _fileHandlerUtil.JsonModel.UserLogin.Remove(getUser);
                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
            return false;
        }

        public List<UserLogin> GetListUsers()
        {
            _fileHandlerUtil.ReadFile(EnumFileConstant.USERLOGIN);

            var listUser = _fileHandlerUtil.JsonModel.UserLogin.ToList();

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);

            return listUser;
        }
    }
}
