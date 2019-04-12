using System;
using System.IO;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Enum;

namespace RememberUtility.Extension
{
    public static class PathHandle
    {
        public static string GetPathOfFile(EnumFileConstant chooseModelForPath)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory; // Set current directory

            var pathAtSlnFile = "";
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            if (directoryInfo.Parent != null && !directoryInfo.Parent.FullName.EndsWith("BigRememberGit"))
            {
                var getFromToEnd = directoryInfo.FullName.Split('\\');
                foreach (var item in getFromToEnd)
                {
                    if (item == "BigRememberGit")
                    {
                        pathAtSlnFile += item + "\\";
                        break;
                    }
                    pathAtSlnFile += item + "\\";
                }
            }
            else
            {
                pathAtSlnFile = directoryInfo.Parent?.FullName;
            }

            var combinePath = "";

            if (chooseModelForPath == EnumFileConstant.BOOKCONSTANT && pathAtSlnFile != null)
            {
                combinePath = Path.Combine(pathAtSlnFile,
                    FileConstant.BookConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.ENTERTAINMENTCONSTANT && pathAtSlnFile != null)
            {
                combinePath = Path.Combine(pathAtSlnFile,
                    FileConstant.EntertainmentConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.EVENTINYEAR && pathAtSlnFile != null)
            {
                combinePath = Path.Combine(pathAtSlnFile,
                    FileConstant.EventInYearConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.QUOTESCONSTANT && pathAtSlnFile != null)
            {
                combinePath = Path.Combine(pathAtSlnFile,
                    FileConstant.QuotesConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.USERLOGIN && pathAtSlnFile != null)
            {
                combinePath = Path.Combine(pathAtSlnFile,
                    FileConstant.UserLoginPath);
            }
            return combinePath;
        }

        public static string GetStandardBrowserPath()
        {
            string browserPath = string.Empty;
            RegistryKey browserKey = null;

            try
            {
                //Read default browser path from Win XP registry key
                browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //If browser path wasn't found, try Win Vista (and newer) registry key
                if (browserKey == null)
                {
                    browserKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                //If browser path was found, clean it
                if (browserKey != null)
                {
                    //Remove quotation marks
                    browserPath = (browserKey.GetValue(null) as string).ToLower().Replace("\"", "");

                    //Cut off optional parameters
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                    }

                    //Close registry key
                    browserKey.Close();
                }
            }
            catch
            {
                //Return empty string, if no path was found
                return string.Empty;
            }
            //Return default browsers path
            return browserPath;
        }

    }
}
