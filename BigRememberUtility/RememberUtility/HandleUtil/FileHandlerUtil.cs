using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ClosedXML.Excel;
using ConnectionSampleCode.Constant;
using ConnectionSampleCode.Enum;
using ConnectionSampleCode.Extension;
using ConnectionSampleCode.Interface;
using ConnectionSampleCode.Model;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConnectionSampleCode.HandleUtil
{
    public class FileHandlerUtil : IFileHandle
    {
        public ConfigModel JsonModel;

        private string _jsonObject;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FileHandlerUtil));

        public FileHandlerUtil()
        {
            JsonModel = new ConfigModel();
        }

        public void ReadFile(EnumFileConstant readEnumFile)
        {
            try
            {
                if (readEnumFile == EnumFileConstant.BOOKCONSTANT)
                {
                    Logger.Info($"[ReadFile] Reading {nameof(Books)} db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.ENTERTAINMENTCONSTAT)
                {
                    Logger.Info($"[ReadFile] Reading {nameof(Entertainment)} db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.QUOTESCONSTANT)
                {
                    Logger.Info($"[ReadFile] Reading ${nameof(Quotes)} db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.EVENTINYEAR)
                {
                    Logger.Info($"[ReadFile] Reading ${nameof(EventInYear)} db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
                else if (readEnumFile == EnumFileConstant.USERLOGIN)
                {
                    Logger.Info($"[ReadFile] Reading ${nameof(UserLogin)} db");
                    var reader = File.ReadAllText(PathHandle.GetPathOfFile(readEnumFile));
                    JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                }
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
                var currentFolder = Directory.GetCurrentDirectory() + @"\JsonDb\" + GetFileName(saveEnumFile);
                Logger.Info("[SaveFile] Begin Save db file");

                if (saveEnumFile == EnumFileConstant.BOOKCONSTANT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new NullToEmptyStringResolver()
                    });

                    File.WriteAllText(currentFolder, string.Empty);
                    using (var sw = new StreamWriter(File.Open(currentFolder, FileMode.Open, FileAccess.Write), Encoding.Unicode))
                    {
                        sw.WriteLine(_jsonObject);
                    }
                }
                else if (saveEnumFile == EnumFileConstant.ENTERTAINMENTCONSTAT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new NullToEmptyStringResolver()
                    });
                    using (var sw = new StreamWriter(File.Open(currentFolder, FileMode.Open, FileAccess.Write), Encoding.Unicode))
                    {
                        sw.WriteLine(_jsonObject);
                    }
                }
                else if (saveEnumFile == EnumFileConstant.QUOTESCONSTANT)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new NullToEmptyStringResolver()
                    });
                    using (var sw = new StreamWriter(File.Open(currentFolder, FileMode.Open, FileAccess.Write), Encoding.Unicode))
                    {
                        sw.Write(_jsonObject);
                    }
                }
                else if (saveEnumFile == EnumFileConstant.EVENTINYEAR)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new NullToEmptyStringResolver()
                    });
                    using (var sw = new StreamWriter(File.Open(currentFolder, FileMode.Open, FileAccess.Write), Encoding.Unicode))
                    {
                        sw.Write(_jsonObject);
                    }
                }
                else if (saveEnumFile == EnumFileConstant.USERLOGIN)
                {
                    _jsonObject = JsonConvert.SerializeObject(JsonModel, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new NullToEmptyStringResolver()
                    });
                    using (var sw = new StreamWriter(File.Open(currentFolder, FileMode.Open, FileAccess.Write), Encoding.Unicode))
                    {
                        sw.Write(_jsonObject);
                    }
                }

                Logger.Info("[SaveFile] Ending Save db file");
            }
            catch (Exception e)
            {
                Logger.Error($"[SaveFile] Unexpected error: '{e.Message}'.");
            }
        }

        public void SaveFileTo(string filePath, string tableName)
        {
            // get from Json db
            var listObject = JsonModel.Books.ToList();

            var dt = ToDataTable(listObject);
            dt.TableName = tableName;

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                // Save file as excel
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

        public void ExportFile<T>(string filePath, string tableName)
        {
            Logger.Info($"[ExportFile] Saving '{Path.GetFileName(filePath)}' file to {Path.GetFullPath(filePath).Split('.')[0]}");

            // Books
            if (typeof(Books).IsAssignableFrom(typeof(T)))
            {
                // Read book json db
                CreateOrReadJsonDb(EnumFileConstant.BOOKCONSTANT);

                // Get data from db
                var listObject = JsonModel.Books.ToList();

                if (listObject != null)
                {
                    var dt = ToDataTable(listObject);
                    dt.TableName = tableName;

                    using (var wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        // Save file as excel
                        wb.SaveAs(filePath);
                    }

                    // Dispose object
                    ReleaseObject(dt);
                    ReleaseObject(listObject);
                }
            }
            // Entertainment
            else if (typeof(Entertainment).IsAssignableFrom(typeof(T)))
            {

                // Get data from db
                var listObject = JsonModel.Entertainment.ToList();
                if (listObject != null)
                {
                    var dt = ToDataTable(listObject);
                    dt.TableName = tableName;

                    using (var wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        // Save file as excel
                        wb.SaveAs(filePath);
                    }

                    // Dispose object
                    ReleaseObject(dt);
                    ReleaseObject(listObject);
                }
            }
            // Quotes
            else if (typeof(IQuote).IsAssignableFrom(typeof(T)))
            {

                if (typeof(IQuote).IsAssignableFrom(typeof(T)))
                {
                    // Get data from db
                    var listObject = JsonModel.Quotes.ToList();

                    if (listObject != null)
                    {
                        var dt = ToDataTable(listObject);
                        dt.TableName = tableName;

                        using (var wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;


                            // Save file as excel
                            wb.SaveAs(filePath);
                        }

                        // Dispose object
                        ReleaseObject(dt);
                        ReleaseObject(listObject);
                    }
                }
            }
            // Event in year
            else if (typeof(IEventInYear).IsAssignableFrom(typeof(T)))
            {
                if (typeof(IEventInYear).IsAssignableFrom(typeof(T)))
                {
                    // Get data from db
                    var listObject = JsonModel.EventInYears.ToList();

                    if (listObject != null)
                    {
                        var dt = ToDataTable(listObject);
                        dt.TableName = tableName;

                        using (var wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;

                            // Save file as excel
                            wb.SaveAs(filePath);
                        }

                        // Dispose object
                        ReleaseObject(dt);
                        ReleaseObject(listObject);
                    }
                }
            }
            // User -> Private
            else if (typeof(IUser).IsAssignableFrom(typeof(T)))
            {
                if (JsonModel.UserLogin.Find(username => username.Username == "admin") != null)
                {
                    // Get data from db
                    var listObject = JsonModel.UserLogin.ToList();

                    if (listObject != null)
                    {
                        var dt = ToDataTable(listObject);
                        dt.TableName = tableName;

                        using (var wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wb.Style.Font.Bold = true;

                            // Save file as excel
                            wb.SaveAs(filePath);
                        }
                        // Dispose object
                        ReleaseObject(dt);
                        ReleaseObject(listObject);
                    }
                }
                else
                {
                    Logger.Warn($"[ExportFile] You cannot use this feature. Please contact with 'Hồ Bình Kha' to get more information!!!");
                }
            }
        }

        /*
        * 1. check if has file and read json file   -> OK
        * 2. If doesn't, create and read json file  -> OK
        * 3. Write as normal                        -> OK
        * 4. Save file                              -> OK
        */
        public void CreateOrReadJsonDb(EnumFileConstant enumFileConstant)
        {
            var fileName = GetFileName(enumFileConstant);

            var currentFileFolder = Directory.GetCurrentDirectory() + "\\JsonDb" + "\\" + fileName;
            if (File.Exists(currentFileFolder))
            {
                if (currentFileFolder.EndsWith(".json") && currentFileFolder.Split('.')[0] != "")
                {
                    // Use bufferedstream for performance reading file
                    using (var fs = File.Open(currentFileFolder, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var bs = new BufferedStream(fs))
                    using (var sr = new StreamReader(bs, true))
                    {
                        var reader = sr.ReadToEnd();
                        JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = new NullToEmptyStringResolver()
                        });
                    }

                    // If null -> Create model as List<object>()
                    CheckModelValue(JsonModel);
                }
            }
            else // filePath doesn't exist (included folder)
            {
                // Use Document document user folder                
                var currentFolder = Directory.GetCurrentDirectory() + @"\JsonDb";
                var jsonConfigModel = JsonConvert.SerializeObject(new ConfigModel(), Formatting.Indented);
                if (fileName.EndsWith(".json") && fileName.Split('.')[0] != "")
                {
                    // Create a json db file
                    Directory.CreateDirectory(currentFolder);
                    File.AppendAllText(currentFolder + "\\" + fileName, jsonConfigModel);

                    using (var fs = File.Open(currentFileFolder, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var bs = new BufferedStream(fs))
                    using (var sr = new StreamReader(bs, true))
                    {
                        var reader = sr.ReadToEnd();
                        JsonModel = JsonConvert.DeserializeObject<ConfigModel>(reader);
                    }
                }
                else
                {
                    // Create a json db file
                    Directory.CreateDirectory(currentFolder);
                    File.AppendAllText(currentFolder + "\\" + fileName + ".json", jsonConfigModel);
                }
            }
        }

        public void CheckModelValue(ConfigModel jsonModel)
        {
            if (jsonModel.Books == null)
            {
                JsonModel.Books = new List<Books>();
            }
            if (jsonModel.Entertainment == null)
            {
                JsonModel.Entertainment = new List<Entertainment>();
            }
            if (jsonModel.Quotes == null)
            {
                JsonModel.Quotes = new List<Quotes>();
            }
            if (jsonModel.EventInYears == null)
            {
                JsonModel.EventInYears = new List<EventInYear>();
            }
            if (jsonModel.Books == null)
            {
                JsonModel.UserLogin = new List<UserLogin>();
            }
        }

        public string GetFileName(EnumFileConstant enumFileConstant)
        {
            string fileName = "";
            if (enumFileConstant == EnumFileConstant.BOOKCONSTANT)
            {
                fileName = FileConstant.BookJson;
            }
            else if (enumFileConstant == EnumFileConstant.ENTERTAINMENTCONSTAT)
            {
                fileName = FileConstant.EntertainmentJson;
            }
            else if (enumFileConstant == EnumFileConstant.QUOTESCONSTANT)
            {
                fileName = FileConstant.QuotesJson;
            }
            else if (enumFileConstant == EnumFileConstant.EVENTINYEAR)
            {
                fileName = FileConstant.EntertainmentJson;
            }
            else if (enumFileConstant == EnumFileConstant.USERLOGIN)
            {
                fileName = FileConstant.UserJson;
            }

            return fileName;
        }

    } // end class


    public class NullToEmptyStringResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                    .Select(p =>
                    {
                        var jp = base.CreateProperty(p, memberSerialization);
                        jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                        return jp;
                    }).ToList();
        }
    }

    public class NullToEmptyStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
            return result;

        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    }

}
