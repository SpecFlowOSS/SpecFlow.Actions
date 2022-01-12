using System.CodeDom;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.CodeDom;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Generator.UnitTestProvider;

namespace Selenium.Targets.Generation
{
    internal class UnitTestTargetFeatureGenerator : UnitTestFeatureGeneratorBase
    {
        public UnitTestTargetFeatureGenerator(IUnitTestGeneratorProvider testGeneratorProvider, CodeDomHelper codeDomHelper, SpecFlowConfiguration specFlowConfiguration, IDecoratorRegistry decoratorRegistry)
            : base(testGeneratorProvider, codeDomHelper, specFlowConfiguration, decoratorRegistry)
        {
            //base.TestClassNameFormat += $"_{_seleniumSpecFlowJsonPart.Browser}";
            base.TestClassNameFormat += $"_does_it_work";
        }

        internal override void SetupScenarioInitializeMethod(TestClassGenerationContext generationContext)
        {
            CodeMemberMethod scenarioInitializeMethod = generationContext.ScenarioInitializeMethod;
            scenarioInitializeMethod.Attributes = MemberAttributes.Public;
            scenarioInitializeMethod.Name = "ScenarioInitialize";
            scenarioInitializeMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(ScenarioInfo), "scenarioInfo"));
            CodeExpression testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            scenarioInitializeMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnScenarioInitialize", new CodeVariableReferenceExpression("scenarioInfo")));

            //scenarioInitializeMethod.Statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("ISeleniumConfiguration config"), new CodeVariableReferenceExpression($"new SeleniumConfiguration {{ Browser = {GetBrowserType(_seleniumSpecFlowJsonPart.Browser)} }}")));
            //scenarioInitializeMethod.Statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("BrowserDriver browserDriver"), new CodeVariableReferenceExpression("new BrowserDriver(config, testRunner.ScenarioContext.ScenarioContainer)")));
            //scenarioInitializeMethod.Statements.Add(new CodeVariableReferenceExpression("testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<BrowserDriver>(browserDriver)"));
        }

        //private string GetBrowserType(Browser browser) 
        //{
        //    return $"{nameof(Browser)}.{browser}";
        //}
    }
}