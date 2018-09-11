using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
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

        public void SaveFileTo(string filePath, string tableName)
        {
            // db
            var listObject = JsonModel.Books.ToList();

            var dt = ToDataTable(listObject);
            dt.TableName = tableName;

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                
                // Save file
                wb.SaveAs(filePath);
            }

            // Dispose object
            ReleaseObject(dt);
            ReleaseObject(listObject);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                if (type == null) return null;

                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch
            {
                // ignored
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
