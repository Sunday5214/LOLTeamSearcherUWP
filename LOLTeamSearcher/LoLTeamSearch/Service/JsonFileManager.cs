using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLTeamSearch.Service
{
    public class JsonFileManager
    {
        public static JObject JsonData = null;

        public JsonFileManager()
        {
        }
        public void ReadJsonFile(string JsonFilePath)
        {
            string jsonData = string.Empty;
            using (StreamReader sw = new StreamReader(JsonFilePath))
            {
                jsonData = sw.ReadToEnd();
            }
            JsonData = JObject.Parse(jsonData);
        }

        public T ConvertJsonToObject<T>(string JsonFilePath)
        {
            string jsonData = string.Empty;
            using (StreamReader sw = new StreamReader(JsonFilePath))
            {
                jsonData = sw.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public List<T> FindData<T>(int data)
        {
            List<T> list = new List<T>();
            var data1 = JsonData["data"].Children().ToList();
            var data2 = data1.Where(x => x.ToString().Contains(data.ToString()) == true);
            if(data2.Count() > 1)
            {
                var items = data2.Children().ToList();
                foreach (var item in items)
                {
                    list.Add(item.ToObject<T>());
                }
            }
            else
            {
                list.Add(data2.Children().ToList()[0].ToObject<T>());
            }
            return list;
        }
    }
}
