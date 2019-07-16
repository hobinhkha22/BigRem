using System;
using System.IO;
using RememberUtility.Constant;
using log4net;
using log4net.Config;

namespace RememberUtility.HandleUtil
{
    public class LoggerUtil
    {
        private static readonly ILog Logs = LogManager.GetLogger(typeof(LoggerUtil));

        public static void HandleLogPath()
        {
            var datetimeNow = $"{DateTime.Now:MMMM dd, yyyy}";

            GlobalContext.Properties[FileConstant.LoggerFileName] = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileConstant.LoggerFolderName + "\\" + datetimeNow);

            var executePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (executePath == null) return;

            executePath = Path.Combine(executePath, FileConstant.LogNetFile);

            if (!File.Exists(executePath)) return;

            var fi = new FileInfo(executePath);

            XmlConfigurator.Configure(fi);
        }

    }

}
