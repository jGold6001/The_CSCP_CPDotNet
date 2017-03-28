using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03___Model
{
    public static class JSONReader
    {
        public static List<string> Brands()
        {
            string path = @"Jsons/Brands.json";
            JObject obj_1 = JObject.Parse(File.ReadAllText(path));
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject obj_2 = (JObject)JToken.ReadFrom(reader);
                JArray array = (JArray)obj_2["brands"];
                return array.ToObject<List<string>>();                
            }
        }

        public static List<string> Colors()
        {
            string path = @"Jsons/Colors.json";
            JObject obj_1 = JObject.Parse(File.ReadAllText(path));
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject obj_2 = (JObject)JToken.ReadFrom(reader);
                JArray array = (JArray)obj_2["colors"];
                return array.ToObject<List<string>>();
            }
        }
    }
}
