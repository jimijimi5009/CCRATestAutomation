using CCRATestAutomation.Environment;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace CCRATestAutomation.CommonPages
{
    internal class JsElements : Config
    {

        public static void JsClick(IWebElement element)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
                js.ExecuteScript("arguments[0].click()", element);
            }
            catch (ElementClickInterceptedException e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public static void HandleAlert()
        {
            try
            {
                Thread.Sleep(2000); // Intentional sleep
                if (IsAlertPresent())
                {
                    driver.SwitchTo().Alert().Accept();
                }
            }
            catch (Exception e)
            {
                // Do nothing
            }
        }
        public static bool IsAlertPresent()
        {
            bool flag = false;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.AlertIsPresent());

                driver.SwitchTo().Alert();
                flag = true;
            }
            catch (NoAlertPresentException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return flag;
        }

        public static void ScrollDown()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,250)");
        }

        public static void ScrollDownToElement(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", element);
        }
    }
}
