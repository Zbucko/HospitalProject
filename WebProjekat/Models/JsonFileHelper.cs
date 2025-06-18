using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public static class JsonFileHelper
    {
        public static List<T> ReadJsonFile<T>(string filePath)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        public static void WriteJsonFile<T>(List<T> data, string filePath)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented,settings);
            File.WriteAllText(filePath, jsonData);
        }

        public static void WriteJsonFile<T>(T data, string filePath)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            File.WriteAllText(filePath, jsonData);
        }
    }
}