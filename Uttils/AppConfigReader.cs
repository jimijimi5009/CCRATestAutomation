
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CCRATestAutomation.Uttils
{
    public class AppConfigReader
    {

        private static IConfiguration Configuration { get; }

        static AppConfigReader()
        {
            string path = Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string jsonFilePath = new Uri(actualPath).LocalPath;
                
            Configuration = new ConfigurationBuilder()
                .SetBasePath(jsonFilePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public static string GetAppSetting(string key)
        {
            return Configuration["AppSettings:" + key];
        }
    }
}
