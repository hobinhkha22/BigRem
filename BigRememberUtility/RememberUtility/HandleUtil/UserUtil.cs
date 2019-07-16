using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.Interface;
using RememberUtility.Model;
using log4net;
using ConnectionSampleCode.Extension;
using RememberUtility.Constant;

namespace RememberUtility.HandleUtil
{
    public class UserUtil : IUser
    {
        private readonly FileHandlerUtil _fileHandlerUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(BooksUtil));

        public UserUtil()
        {
            _fileHandlerUtil = new FileHandlerUtil();
            _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.USERLOGIN);
        }

        public void AddUser(UserLogin userLogin)
        {
            if (userLogin != null)
            {
                var checkDuplicate = CheckUser(userLogin.Username);
                if (checkDuplicate == null)
                {
                    userLogin.CreatedDate = $"{DateTime.Now:MMMM dd,yyyy}";
                    userLogin.UserId = HandleRandom.RandomString(10);
                    Encrypter.Encrypt(userLogin.PasswordEncrypt, UserConstant.KeyEncrypt);
                    userLogin.UserRole = UserRoleEnum.NormalUser;

                    _fileHandlerUtil.JsonModel.UserLogin.Add(userLogin);

                    _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);

                    Logs.Info($"[AddUser] Adding '{userLogin.Username}' successful.");

                    _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.USERLOGIN);
                }
            }
            else
            {
                // Duplicate user name
                Logs.Warn($"[AddUser] '{userLogin.Username}' have duplicate. Add failed!");
                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
            }
        }

        public UserLogin CheckUser(string username, string password)
        {
            try
            {
                return _fileHandlerUtil.JsonModel.UserLogin.Find(x => x.Username.ToLower() == username.ToLower());
            }
            catch (Exception)
            {
                Logs.Warn($"[CheckUser] The user '{username}' doesn't exist.");

                return null;
            }
        }

        public bool UpdateUser(string currentUserName, string newUserName, string newPassword)
        {
            var getCurrentUser = _fileHandlerUtil.JsonModel.UserLogin.
                Find(x => string.Equals(x.Username, currentUserName, StringComparison.CurrentCultureIgnoreCase));
            var indexOfUser = _fileHandlerUtil.JsonModel.UserLogin.IndexOf(getCurrentUser);

            if (getCurrentUser != null)
            {
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].Username = newUserName;
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].PasswordEncrypt = Encrypter.Encrypt(newPassword, UserConstant.KeyEncrypt); // encrypt and save
                _fileHandlerUtil.JsonModel.UserLogin[indexOfUser].LastModifiedDate = $"{DateTime.Now:MMMM dd, yyyy}";

                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
                Logs.Info($"[UpdateUser] Updating '{newUserName}' successful.");

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
            Logs.Warn($"[UpdateUser] Updating '{newUserName}' failed.");

            return false;
        }

        public bool DeleteUser(string username)
        {
            if (username == null) return false;

            var getUser = _fileHandlerUtil.JsonModel.UserLogin.Find(x =>
                string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));

            if (getUser != null)
            {
                _fileHandlerUtil.JsonModel.UserLogin.Remove(getUser);

                _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
                Logs.Info($"[DeleteUser] Deleting '{username}' successful.");

                return true;
            }

            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
            Logs.Warn($"[DeleteUser] Deleting '{username}' failed.");

            return false;
        }

        public List<UserLogin> GetListUsers()
        {
            try
            {
                return _fileHandlerUtil.JsonModel.UserLogin.ToList();
            }
            catch (Exception)
            {
                Logs.Error($"[GetListUsers] Error while getting user list.");

                return null;
            }
        }

        public void SaveUserDb()
        {
            _fileHandlerUtil.SaveFile(EnumFileConstant.USERLOGIN);
        }

        public UserLogin CheckUser(string username)
        {
            try
            {
                return _fileHandlerUtil.JsonModel.UserLogin.
                Find(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception)
            {
                Logs.Warn($"[CheckUser] The user '{username}' doesn't exist.");

                return null;
            }
        }

        public void ReloadDatbase()
        {
            _fileHandlerUtil.CreateOrReadJsonDb(EnumFileConstant.USERLOGIN);
        }
    }
}
