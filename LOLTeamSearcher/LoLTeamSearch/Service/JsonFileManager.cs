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
        string JsonFilePath = string.Empty;
        JObject JsonData = default;
        public JsonFileManager(string FilePath)
        {
            JsonFilePath = FilePath;
        }

        public void ReadJsonFile()
        {
            string jsonData = string.Empty;
            using (StreamReader sw = new StreamReader(JsonFilePath))
            {
                jsonData = sw.ReadToEnd();
            }
            JsonData = JObject.Parse(jsonData);
        }

        public void FindData(int data)
        {
            //JsonData.
        }
    }
}
