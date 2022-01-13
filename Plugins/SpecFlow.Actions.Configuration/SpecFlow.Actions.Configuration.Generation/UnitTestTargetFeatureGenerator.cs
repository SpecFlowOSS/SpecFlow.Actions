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
        private readonly string _target;

        public UnitTestTargetFeatureGenerator(IUnitTestGeneratorProvider testGeneratorProvider,
            CodeDomHelper codeDomHelper, SpecFlowConfiguration specFlowConfiguration,
            IDecoratorRegistry decoratorRegistry, string target)
            : base(testGeneratorProvider, codeDomHelper, specFlowConfiguration, decoratorRegistry)
        {
            _target = target;
            //base.TestClassNameFormat += $"_{_seleniumSpecFlowJsonPart.Browser}";
            base.TestClassNameFormat += $"_{target.Replace(".", "_")}";
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
            scenarioInitializeMethod.Statements.Add(new CodeSnippetStatement($"\t\t\ttestRunner.ScenarioContext[\"__SpecFlowActionsConfigurationTarget\"] = \"{_target}\";"));
        }

        //private string GetBrowserType(Browser browser) 
        //{
        //    return $"{nameof(Browser)}.{browser}";
        //}
    }
}