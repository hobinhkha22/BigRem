using System;
using System.IO;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Enum;

namespace ConnectionSampleCode.Extension
{
    public static class PathHandle
    {
        private static readonly string PathAtSlnFile = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
        
        public static string GetPathOfFile(EnumFileConstant chooseModelForPath)
        {
            var combinePath = "";
            if (chooseModelForPath == EnumFileConstant.BOOKCONSTANT)
            {
                combinePath = Path.Combine(PathAtSlnFile ?? throw new InvalidOperationException(), FileConstant.BookConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.ENTERTAINMENTCONSTAT)
            {
                combinePath = Path.Combine(PathAtSlnFile, FileConstant.EntertainmentConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.EVENTINYEAR)
            {
                combinePath = Path.Combine(PathAtSlnFile, FileConstant.EventInYearConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.QUOTESCONSTANT)
            {
                combinePath = Path.Combine(PathAtSlnFile, FileConstant.QuotesConstantPath);
            }
            else if (chooseModelForPath == EnumFileConstant.USERLOGIN)
            {
                combinePath = Path.Combine(PathAtSlnFile, FileConstant.UserLoginPath);
            }

            return combinePath;
        }
    }
}
