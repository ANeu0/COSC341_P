using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CSVHelper
    {
        public static string CSVName { get; set; }
        public static void initCSV<T>(this T obj, string filename)
        {
            if (!File.Exists(filename))
            {
                IList<string> headers = GetFieldNames(obj);
                string headerLine = CreateCSVLine(headers);
                WriteToFile(headerLine, filename);
            }
        }

        public static void WriteObjToCSV<T>(this T obj, string filename)
        {
            Type type = typeof(T);
            var objValues = GetFieldValues<T>(obj);

            IList<string> content = new List<string>();
            foreach (string value in objValues)
            {
                content.Add(value);
            }
            WriteToFile(CreateCSVLine(content), filename);
        }

        public static IList<string> GetFieldNames<T>(this T obj)
        {
            Type type = typeof(T);
            var properties = GetFieldNames<T>();
            IList<string> result = new List<string>();

            foreach (string property in properties)
            {
                result.Add(property);
            }

            return result;
        }

        private static string CreateCSVLine(IList<string> headers)
        {
            string headerLine = string.Empty;
            // if the property is the first, don't add a comma prefix
            for (int i = 0; i < headers.Count; i++)
            {
                if (i != 0)
                {
                    headerLine = String.Concat(headerLine, ",", headers[i]);
                }
                else
                {
                    headerLine = String.Concat(headerLine, headers[i]);
                }
            }
            return headerLine;
        }
        private static void WriteToFile(string content, string filename)
        {
            using (TextWriter textWriter = new StreamWriter(filename, true))
            {
                textWriter.WriteLine(content);
            }
        }

        private static string[] GetFieldNames<T>()
        {
            return typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(field => field.Name)
                            .ToArray();
        }

        private static object[] GetFieldValues<T>(T obj)
        {
            return typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(field => field.GetValue(obj))
                            .ToArray();
        }
    }
}
