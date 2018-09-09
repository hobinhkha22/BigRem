using System;
using System.Diagnostics;
using System.IO;
using ConnectionSampleCode.Constant;
using log4net;
using log4net.Config;

namespace ConnectionSampleCode.HandleUtil
{
    public class LoggerUtil
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoggerUtil));

        public static void HandleLogPath(string folderName, string logfilename)
        {
            GlobalContext.Properties[FileConstant.LoggerFileName] = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileConstant.LoggerFolderName + "\\" + FileConstant.LoggerName);

            Logger.Info($"[HandlePathLog] Path combine: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileConstant.LoggerFolderName + "\\" + FileConstant.LoggerName)}");

            var executePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Logger.Debug($"[HandlePathLog] Execute path: {executePath}");

            if (executePath == null) return;

            executePath = Path.Combine(executePath, FileConstant.LogNetFile);

            if (!File.Exists(executePath)) return;

            var fi = new FileInfo(executePath);
            Logger.Debug($"[HandlePathLog] fileinfo directory: {fi.Directory}");
            Logger.Debug($"[HandlePathLog] fileinfo directory name: {fi.DirectoryName}");
            XmlConfigurator.ConfigureAndWatch(fi);
            
            Debug.WriteLine($" Logger here: {GlobalContext.Properties[FileConstant.LoggerFileName] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileConstant.LoggerFolderName + "\\" + FileConstant.LoggerName)}");
        }



    }

}
