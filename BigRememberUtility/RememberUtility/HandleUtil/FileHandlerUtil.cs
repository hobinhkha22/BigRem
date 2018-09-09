using System;
using System.IO;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;
using log4net;
using Newtonsoft.Json;

namespace ConnectionSampleCode.HandleUtil
{
    public class FileHandlerUtil : IFileHandle
    {
        public ConfigModel JsonModel;

        private string _jsonObject;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FileHandlerUtil));

        public void ReadFile(EnumFileConstant readEnumFile)
        {
            try
            {
                Logger.Info("[ReadFile] Begin reading file");

                if (readEnumFile == EnumFileConstant.BOOKCONSTANT)
                {
                    Logger.Info("[ReadFile] Reading Book db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.ENTERTAINMENTCONSTAT)
                {
                    Logger.Info("[ReadFile] Reading Entertainment db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.QUOTESCONSTANT)
                {
                    Logger.Info("[ReadFile] Reading Quote db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.EVENTINYEAR)
                {
                    Logger.Info("[ReadFile] Reading Event in year db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.USERLOGIN)
                {
                    Logger.Info("[ReadFile] Reading User db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                Logger.Info("[ReadFile] Ending reading file");
            }
            catch (Exception e)
            {
                Logger.Error($"[ReadFile] Unexpected error: {e.Message}");
            }
        }

        public void SaveFile(EnumFileConstant saveEnumFile)
        {
            try
            {
                Logger.Info("[SaveFile] Begin Save file db");
                if (saveEnumFile == EnumFileConstant.BOOKCONSTANT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented);
                    File.WriteAllText(PathHandle.GetPathOfFile(EnumFileConstant.BOOKCONSTANT), _jsonObject);
                }
                else if (saveEnumFile == EnumFileConstant.ENTERTAINMENTCONSTAT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented);
                    File.WriteAllText(PathHandle.GetPathOfFile(EnumFileConstant.ENTERTAINMENTCONSTAT), _jsonObject);
                }
                else if (saveEnumFile == EnumFileConstant.QUOTESCONSTANT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented);
                    File.WriteAllText(PathHandle.GetPathOfFile(EnumFileConstant.QUOTESCONSTANT), _jsonObject);
                }
                else if (saveEnumFile == EnumFileConstant.EVENTINYEAR)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented);
                    File.WriteAllText(PathHandle.GetPathOfFile(EnumFileConstant.EVENTINYEAR), _jsonObject);
                }
                else if (saveEnumFile == EnumFileConstant.USERLOGIN)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented);
                    File.WriteAllText(PathHandle.GetPathOfFile(EnumFileConstant.USERLOGIN), _jsonObject);
                }

                Logger.Info("[SaveFile] Ending Save file db");
            }
            catch (Exception e)
            {
                Logger.Error($"[SaveFile] Unexpected error: {e.Message}");
            }
        }


    }
}
