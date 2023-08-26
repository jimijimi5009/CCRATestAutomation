using CCRATestAutomation.CommonPages;

namespace CCRATestAutomation.StepDefinitions
{
    [Binding]
    public  class CalculatorStepDefinitions
    {

         Helper helper;

        public CalculatorStepDefinitions(Helper helper )
        {
            this.helper = helper;
        }

        [Given(@"i navigate to google")]
        public void GivenINavigateToGoogle()
        {

            helper.GetCalculatorSPage().enterData("i am here");
        }


    }
}