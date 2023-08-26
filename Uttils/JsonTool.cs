using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CCRATestAutomation.Uttils
{
    internal class JsonTool
    {

        public static string ReadJsonFile(string jsonFileName)
        {
            try
            {
  
                string path = Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string jsonFilePath = new Uri(actualPath).LocalPath;
                string filePath = jsonFilePath + "TestData\\"+ jsonFileName;

                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    return jsonContent;
                }
                else
                {
                    throw new FileNotFoundException("JSON file not found.", filePath);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static string GetSpecificValue(string jsonContent, string propertyName)
        {
            try
            {
                JObject jsonObject = JObject.Parse(jsonContent);
                JToken value = jsonObject[propertyName];

                if (value != null)
                {
                    return value.ToString();
                }
                else
                {
                    throw new Exception("Property not found in JSON.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }




    }
}
