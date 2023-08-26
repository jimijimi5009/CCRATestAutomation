using CCRATestAutomation.CommonPages;
using OpenQA.Selenium;


namespace CCRATestAutomation.PageObjects
{
    public class CalculatorSPage
    {

        private WebDriver driver;

        private IWebElement textBox => driver.FindElement(By.XPath("//*[@type='search']"));

        public CalculatorSPage(WebDriver driver)
        {
            this.driver = driver;
        }

        public void enterData(string data)
        {
            SeleniumElements.WaitAndSendKeys(textBox, data);

            Console.WriteLine(data);
           
        }
    }
}
