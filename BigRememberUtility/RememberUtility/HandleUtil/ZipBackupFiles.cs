using Ionic.Zip;
using log4net;
using RememberUtility.Enum;
using RememberUtility.HandleUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.HandleUtil
{
    public class ZipBackupFiles
    {
        private static readonly ILog Logs = LogManager.GetLogger(typeof(ZipBackupFiles));
        public static void ZipFile(EnumFileConstant zipEnumFile)
        {
            try
            {
                var fileUtil = new FileHandlerUtil();

                var currentFile = Directory.GetCurrentDirectory() + @"\JsonDb\" + fileUtil.GetFileName(zipEnumFile);

                var names = zipEnumFile.ToString();
                var placeToArchive = Directory.GetCurrentDirectory() + @"\BackupDb\";
                var saveZipTo = placeToArchive + names + ".zip";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(currentFile, names);
                    zip.Save(saveZipTo);
                }
            }
            catch (Exception a)
            {
                Logs.Error($"[ZipFile] Error while saving zip file: {a.Message}");
            }
        }
    }
}
