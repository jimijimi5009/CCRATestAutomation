using CCRATestAutomation.CommonPages;
using CCRATestAutomation.Environment;
using SpecFlow;
using TechTalk.SpecFlow;

namespace CCRATestAutomation.Hooks
{

    [Binding]
    public class Hooks : Config
    {
         private Helper helper;
        private ScenarioContext _scenarioContext;


        public Hooks(Helper helper, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this.helper = helper;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            helper.startApplication();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            bool x = _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError;
            helper.CloseResourceForCompleted(_scenarioContext);
        }

    }
}
