﻿using System;
using System.Diagnostics;
using System.IO;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;
using Newtonsoft.Json;

namespace ConnectionSampleCode.HandleUtil
{
    public class FileHandlerUtil : IFileHandle
    {
        public ConfigModel JsonModel;
        
        private string _jsonObject;

        public void ReadFile(EnumFileConstant readEnumFile)
        {
            try
            {
                if (readEnumFile == EnumFileConstant.BOOKCONSTANT)
                {
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.ENTERTAINMENTCONSTAT)
                {
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.QUOTESCONSTANT)
                {
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.EVENTINYEAR)
                {
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.USERLOGIN)
                {
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("[ReadFile] Error read file: " + e.Message);
                Console.WriteLine("Error unexpected to read file: " + e.Message);
            }
        }

        public void SaveFile(EnumFileConstant saveEnumFile)
        {
            try
            {
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
            }
            catch (Exception e)
            {
                Debug.WriteLine("[SaveFile] Error SaveFile: " + e.Message);
                Console.WriteLine("Error unexpected to save file" + e.Message);
            }
        }


    }
}