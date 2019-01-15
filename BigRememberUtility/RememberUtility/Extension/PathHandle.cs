using System;
using System.IO;
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
            else if (chooseModelForPath == EnumFileConstant.ENTERTAINMENTCONSTAT && pathAtSlnFile != null)
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


    }
}
