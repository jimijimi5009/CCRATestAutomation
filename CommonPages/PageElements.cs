using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCRATestAutomation.Environment;

namespace CCRATestAutomation.CommonPages
{
    internal class PageElements : Config
    {

        public static void CheckIfPageIsReady()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string pageState = null;
            do
            {
                pageState = js.ExecuteScript("return document.readyState").ToString();
            } while (!pageState.Equals("complete", StringComparison.OrdinalIgnoreCase));
        }

        public static void ScrollDown()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,250)");
        }

        public static void ScrollDownToElement(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ClickElementUsingAction(IWebElement element, IWebElement clickElem)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            // You might need to wait here before clicking
            clickElem.Click();
        }

        public static void SelectFromDropDown(IWebElement element, string testdata)
        {
            try
            {
                //SeleniumElements.WaitUntilClickable(element);
                SelectElement option = new SelectElement(element);
                option.SelectByText(testdata);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void SelectFromDropDownByVal(IWebElement element, string testdata)
        {
            try
            {
                // SeleniumElements.WaitUntilClickable(element);
                SelectElement option = new SelectElement(element);
                option.SelectByValue(testdata);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string ExtractNumberFromString(string number)
        {
            string num = System.Text.RegularExpressions.Regex.Replace(number, "[^0-9]+", " ");
            return num.Replace(" ", "");
        }

       
    }
}
