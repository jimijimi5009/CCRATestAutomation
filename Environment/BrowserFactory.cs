using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using CCRATestAutomation.Uttils;
using Serilog;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using WebDriverManager.Helpers;

namespace CCRATestAutomation.Environment
{
    internal class BrowserFactory : Config
    {



        public BrowserFactory() {

            Log.Logger = new LoggerConfiguration()

                                           .WriteTo.Console()
                                           .MinimumLevel.Debug()
                                           .CreateLogger();
        }

        public IWebDriver GetBrowser( )
        {
            try
            {
                //string path = Assembly.GetCallingAssembly().CodeBase;
                //string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                //string jsonFilePath = new Uri(actualPath).LocalPath;
                //string filePath = jsonFilePath + "TestData\\BrowsersList.json";


                string browser = GetBrowserName();
                string osName = OsUtill.GetOperatingSystem();
                if (osName.Contains("Win"))
                {
                    OsUtill.KillAllProcesses(browser);
                }
                driver= (WebDriver)GetLocalDriver(browser, osName);
                driver.Manage().Window.Maximize();
                return driver;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebDriver GetLocalDriver(string browser, string osName)
        {
            try
            {
                switch (browser.ToLower())
                {
                    case "chrome":
                        if (osName.Contains("Win"))
                        {
                            driver= (WebDriver)SetChromeDriverForWin();
                        }
                        else if (osName.Contains("Mac"))
                        {
                            SetChromeDriverForMac();
                        }
                        break;
                    case "firefox":
                        SetFirefoxDriver();
                        break;
                    case "edge":
                        SetEdgeDriver();
                        break;
                    default:
                        Console.WriteLine(browser + " is not a supported browser");
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            return driver;
        }
        public string GetBrowserName()
        {
            try
            {
                string jsonContent = JsonTool.ReadJsonFile("BrowsersList.json");            
                string browserName = JsonTool.GetSpecificValue(jsonContent, "Browser");
                

                return browserName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
   
        private IWebDriver SetChromeDriverForWin()
        {
            try
            {
                var options = new ChromeOptions();
                options.AddArguments("test-type");
                options.AddArguments("start-maximized");
                options.AddArguments("--enable-precise-memory-info");
                options.AddArguments("--disable-popup-blocking");
                options.AddArguments("--disable-default-apps");
                options.AddArguments("test-type=browser");
                options.AddArguments("--incognito");

                new DriverManager().SetUpDriver(new ChromeConfig());
               // new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                var driver = new ChromeDriver(options);

                Console.WriteLine("Starting Chrome browser");
                return driver;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        private IWebDriver SetChromeDriverForMac()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                string downloadFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "browser_downloads");
                Dictionary<string, object> chromePrefs = new Dictionary<string, object>
                {
                    ["profile.default_content_settings.popups"] = 0,
                    ["download.default_directory"] = downloadFilepath
                };
                options.AddArguments("test-type");
                options.AddArguments("disable-extensions");
                options.AddArguments("--ignore-certificate-errors");
                options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
                options.AddUserProfilePreference("download.default_directory", downloadFilepath);
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                driver = new ChromeDriver(options);
                return driver;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebDriver SetFirefoxDriver()
        {
            try
            {
                FirefoxOptions opts = new FirefoxOptions();
                opts.AddArguments("-private");
                new DriverManager().SetUpDriver(new FirefoxConfig(), version: "latest");
                driver = new FirefoxDriver(opts);
                Console.WriteLine("Starting Firefox browser");
                return driver;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebDriver SetEdgeDriver()
        {
            try
            {
                EdgeOptions edgeOptions = new EdgeOptions();
                edgeOptions.AddArgument("-inprivate");
                new DriverManager().SetUpDriver(new EdgeConfig(), version: "latest");
                driver = new EdgeDriver(edgeOptions);
                Console.WriteLine("Starting Edge browser");
                return driver;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

    }
}
