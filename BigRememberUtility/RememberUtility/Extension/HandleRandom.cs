using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;

namespace RememberUtility.Extension
{
    public static class HandleRandom
    {

        /// <summary>Indicates whether the specified array is null or has a length of zero.</summary>
        /// <param name="array">The array to test.</param>
        /// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }

        /// <summary>
        /// Return a string with numbers and letters
        /// </summary>
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray()).ToLower();
        }

        public static void ChooseColorForString(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static string GetDateTimeNow()
        {
            var handleDate = DateTime.Now.ToString("MMMMdd_yyyy");
            return handleDate;
        }

        /// <summary>
        /// Get all public constant value
        /// </summary>
        /// <para>https://stackoverflow.com/questions/10261824/how-can-i-get-all-constants-of-a-type-by-reflection</para>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        public static void ExportExcel<T>(List<T> listObject, string worksheetName, string path)
        {
            DataTable dt = ToDataTable(listObject);

            // Handle worksheet
            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, worksheetName);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                // Save workbook                
                GetStream(wb, path);
            }
        }

        public static void GetStream(XLWorkbook excelWorkbook, string path)
        {
            using (MemoryStream fs = new MemoryStream())
            {
                excelWorkbook.SaveAs(fs);
                fs.Position = 0;
                FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.WriteTo(fileStream);
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        /// <summary>
        /// Remove string holds 'http:' or 'https:' in string
        /// </summary>
        /// <param name="httpString">http or https URL</param>
        /// <returns>A string removed http or https</returns>
        public static string RemoveHttpString(string httpString)
        {
            if (httpString != "")
            {
                if (httpString.ToLower().StartsWith("http") || httpString.ToLower().StartsWith("https"))
                {
                    var uri = new Uri(httpString);
                    var host = uri.Host + uri.AbsolutePath;

                    if (host.StartsWith("www"))
                    {
                        return host.Replace("www.", "");
                    }

                    return host;
                }
            }

            return httpString;
        }

    } // end class
}
