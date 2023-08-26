using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCRATestAutomation.Environment;
using CCRATestAutomation.Uttils;
using NUnit.Framework;
using Gherkin;

namespace CCRATestAutomation.CommonPages
{
    public class SeleniumElements : Config
    {

        public void WaitUntilElementIsClickable(IWebElement element)
        {
           
            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime))
                .Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitUntilClickable(IWebElement element)
        {
            try
            {
                WaitUntilElementIsClickable(element);
            }
            catch (Exception)
            {
                WaitUntilElementIsClickable(element);
            }
        }

        public void WaitAndClick(IWebElement element)
        {
            WaitUntilClickable(element);
            element.Click();
        }

        public void WaitUntilTextIsVisible(IWebElement element, string testData)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime))
                .Until(ExpectedConditions.TextToBePresentInElement(element, testData));
        }

        public void WaitUntilElementIsVisible(By locator)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitUntilElementIsVisible(string elementXPath)
        {
            try
            {

                long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
                new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime))
                    .Until(ExpectedConditions.ElementIsVisible(By.XPath(elementXPath)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

     

        public static bool IsElementPresent(IWebElement element)
        {
            return element != null;
        }

        public static bool IsElePresent(By testData)
        {
            bool flag = false;
            try
            {
                flag = driver.FindElements(testData).Count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return flag;
        }

        public static bool IsCssVisible(By testData)
        {
            bool flag = false;
            try
            {
                string elementStyle = driver.FindElement(testData).GetAttribute("style");
                flag = !(elementStyle.Equals("display: none;") || elementStyle.Equals("visibility: hidden"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return flag;
        }

        public static IWebElement EligibilityOrAvaility(By para1, By para2)
        {
            IWebElement element = null;
            IWebElement element1;
            IWebElement element2;

            do
            {
                try
                {
                    element1 = driver.FindElement(para1);
                }
                catch (Exception)
                {
                    element1 = null;
                }
                try
                {
                    element2 = driver.FindElement(para2);
                }
                catch (Exception)
                {
                    element2 = null;
                }

                if (element1 != null)
                {
                    element = element1;
                }
                else if (element2 != null)
                {
                    element = element2;
                }
            } while (element == null);

            return element;
        }

        public static IList<IWebElement> GetDropdownListItems(By testData)
        {
            return new SelectElement(driver.FindElement(testData)).Options;
        }

        public static void NavigateBackToPreviousPage()
        {
            driver.Navigate().Back();
        }

        public static IWebElement GetRootElement(IWebElement element)
        {
            return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].shadowRoot", element);
        }

        public static string InitCap(string testData)
        {
            return char.ToUpper(testData[0]) + testData.Substring(1).ToLower();
        }



        public static async Task WaitFor(int waitMillis)
        {
            await Task.Delay(waitMillis);
        }

        public static void ExplicitlyWaitForElementToBeClickable(IWebElement ele)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(ExpectedConditions.ElementToBeClickable(ele));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to Get the element After waiting {waitTime} Second...");
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void WaitAndSendKeys(IWebElement element, string testData)
        {
            ExplicitlyWaitForElementToBeClickable(element);
            element.SendKeys(testData);
        }

        public static void WaitForElementText(IWebElement ele, string text)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
            wait.Until(ExpectedConditions.TextToBePresentInElement(ele, text));
        }

        public static void ExplicitlyWaitForElementVisibility(By ele)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ele));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to Get the element After waiting {waitTime} Second...");
                Console.WriteLine(e.StackTrace);
            }
        }
        public static bool ExplicitlyWaitForElementText(IWebElement ele, string text)
        {

            long waitTime = long.Parse(AppConfigReader.GetAppSetting("WaitTime"));
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                return wait.Until(ExpectedConditions.TextToBePresentInElement(ele, text));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Get the Expected Text After waiting " + waitTime + "Second...");
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static void HighlightElementBackground(IWebElement element, string flag)
        {
            // Get the JavascriptExecutor instance.
            var js = (IJavaScriptExecutor)driver;

            // Highlight the element based on the flag.
            if (flag.Equals("pass"))
            {
                // js.executeScript("arguments[0].style.border='1px groove green'", element);
                js.ExecuteScript("arguments[0].style.backgroundColor = '" + "yellow" + "'", element);
            }
            else
            {
                // js.executeScript("arguments[0].style.border='4px solid red'", element);
                js.ExecuteScript("arguments[0].style.backgroundColor = '" + "red" + "'", element);
            }

            // Wait for 2 seconds.
            Thread.Sleep(2000);
        }

        public static string GetText(IWebElement element)
        {
            //highlightElementBackground(element, "pass");
            string getElement = null;
            try
            {
                getElement = element.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return getElement;
        }

        public static void ClickWithStrElement(string xpath)
        {
            try
            {
                driver.FindElement(By.XPath(xpath)).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void ScrollToElement(IWebElement webElement) 
        {

            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoViewIfNeeded()", webElement);
                Thread.Sleep(500);
            }
            catch (Exception)
            {

                throw;
            } 
         }

        public static void ClickFromElements(IList<IWebElement> relements, string text)
        {
            for (int i = 0; i < relements.Count; i++)
            {
                string x = relements.ElementAt(i).Text;
                if (x.ToLower().Contains(text.ToLower()) || x.Contains(text))
                {
                    relements.ElementAt(i).Click();
                }
            }
        }

        public static void ClickFromElementsWithEquleText(IList<IWebElement> relements, string text)
        {
            for (int i = 0; i < relements.Count; i++)
            {
                string x = relements.ElementAt(i).Text;
                if (x.ToLower().Equals(text.ToLower()))
                {
                    relements.ElementAt(i).Click();
                }
            }
        }

        public static IList<string> GetAllText(IList<IWebElement> relements)
        {
            IList<string> all_elements_text = new List<string>();

            for (int i = 0; i < relements.Count; i++)
            {
                all_elements_text.Add(relements.ElementAt(i).Text);
            }

            return all_elements_text;
        }

        public static IList<string> GetAllAttributeText(IList<IWebElement> relements, string attbName)
        {
            IList<string> all_elements_text = new List<string>();

            for (int i = 0; i < relements.Count; i++)
            {
                all_elements_text.Add(relements.ElementAt(i).GetAttribute(attbName));
            }

            return all_elements_text;
        }

        public static List<string> GetSubstringFromText(List<string> strData, string openingStr, string closingStr)
        {
            List<string> all_elements_text = new List<string>();
            for (int i = 0; i < strData.Count; i++)
            {
                int start = strData[i].IndexOf(openingStr);
                if (start == -1)
                {
                    continue;
                }

                int end = strData[i].IndexOf(closingStr, start + openingStr.Length);
                if (end == -1)
                {
                    continue;
                }

                all_elements_text.Add(strData[i].Substring(start + openingStr.Length, end - start - openingStr.Length));
            }
            return all_elements_text;
        }

        public static string GetSubstringBetween(string strData, string openingStr, string closingStr)
        {
            int start = strData.IndexOf(openingStr);
            if (start == -1)
            {
                return null;
            }

            int end = strData.IndexOf(closingStr, start + openingStr.Length);
            if (end == -1)
            {
                return null;
            }

            return strData.Substring(start + openingStr.Length, end - start - openingStr.Length);
        }
        public static void ClickButton(IWebElement element)
        {
       
            element.Click();
        }

        public static void SendKeys(IWebElement element, string data)
        {
            
            //highlightElementBackground(element, "pass");
            element.SendKeys(data);
        }

        public static List<string> GetAllText( IWebDriver driver, string xpathStr)
        {
            IReadOnlyCollection<IWebElement> elementList = driver.FindElements(By.XPath(xpathStr));
            List<IWebElement> listFromUi = new List<IWebElement>(elementList);
            List<string> validations = new List<string>();


            for (int i = 0; i < listFromUi.Count; i++)
            {

                validations.Add(listFromUi[i].Text);

            }
            return validations;
        }
        public static List<string> getAllTExtFromUi( IReadOnlyCollection<IWebElement> element)
        {

            //List<IWebElement> listFromUi = new List<IWebElement>(element);
            List<string> text = new List<string>();


            for (int i = 0; i < element.Count; i++)
            {
                text.Add(element.ElementAt(i).Text);

            }
            return text;
        }


     

        public static void SendKeyWithMethord( IWebElement element, string text)
        {

            element.Clear();
            element.SendKeys(text);

        }

        public static bool IsDisplayedMethord( IWebElement element)
        {
            return element.Displayed;

        }

        public static List<String> GetTableData(Table table, string getData)
        {
            var getlistFromTable = from row in table.Rows select row[getData];
            List<string> listfromFetureFile = getlistFromTable.ToList();
            return listfromFetureFile;

        }
        public static void SelectByVal( IWebElement element, String value)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(value);

        }

        public static void WaitUntillEleClickAble( IWebDriver driver, IWebElement element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitAndClick( IWebDriver driver, IWebElement element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }
    }


}

