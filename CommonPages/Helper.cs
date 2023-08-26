using CCRATestAutomation.Environment;
using CCRATestAutomation.PageObjects;
using CCRATestAutomation.Uttils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRATestAutomation.CommonPages
{
    public class Helper
    {
        WebDriver driver;

        CalculatorSPage calculatorSPage;


        public void startApplication()
        {

           
            //String executionEnvironment = new EnvironmentFactory().getBrowserExecutionEnvironment();
            //String appEnv = new EnvironmentFactory().getApplicationEnv(); ///QA, Stage;

            this.driver = (WebDriver)new BrowserFactory().GetBrowser();
            string url = AppConfigReader.GetAppSetting("url");

            driver.Navigate().GoToUrl(url);

        }



        public CalculatorSPage GetCalculatorSPage()
        {
            if (calculatorSPage == null)
            {
                calculatorSPage = new CalculatorSPage(driver);
            }
            return calculatorSPage;
        }




        public void CloseResourceForCompleted(ScenarioContext scenarioContext)
        {
            // Close the Driver
            driver.Quit();

            // Take a screenshot for failed scenarios
            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                scenarioContext.Add("screenshot", screenshot.AsBase64EncodedString);
            }
        }

    }
}
