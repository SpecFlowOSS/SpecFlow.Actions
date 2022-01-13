using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.CodeDom;
using TechTalk.SpecFlow.Generator.Generation;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Parser;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlow.Actions.Configuration.Generation
{
    public abstract class UnitTestFeatureGeneratorBase : IFeatureGenerator
    {
        private readonly CodeDomHelper _codeDomHelper;

        private readonly IDecoratorRegistry _decoratorRegistry;

        internal readonly ScenarioPartHelper _scenarioPartHelper;

        private readonly SpecFlowConfiguration _specFlowConfiguration;

        private readonly IUnitTestGeneratorProvider _testGeneratorProvider;

        private readonly UnitTestMethodGenerator _unitTestMethodGenerator;

        private readonly LinePragmaHandler _linePragmaHandler;

        public string TestClassNameFormat { get; set; } = "{0}Feature";


        public UnitTestFeatureGeneratorBase(IUnitTestGeneratorProvider testGeneratorProvider, CodeDomHelper codeDomHelper, SpecFlowConfiguration specFlowConfiguration, IDecoratorRegistry decoratorRegistry)
        {
            _testGeneratorProvider = testGeneratorProvider;
            _codeDomHelper = codeDomHelper;
            _specFlowConfiguration = specFlowConfiguration;
            _decoratorRegistry = decoratorRegistry;
            _linePragmaHandler = new LinePragmaHandler(_specFlowConfiguration, _codeDomHelper);
            _scenarioPartHelper = new ScenarioPartHelper(_specFlowConfiguration, _codeDomHelper);
            _unitTestMethodGenerator = new UnitTestMethodGenerator(testGeneratorProvider, decoratorRegistry, _codeDomHelper, _scenarioPartHelper, _specFlowConfiguration);
        }

        public CodeNamespace GenerateUnitTestFixture(SpecFlowDocument document, string testClassName, string targetNamespace)
        {
            var codeNamespace = CreateNamespace(targetNamespace);
            var specFlowFeature = document.SpecFlowFeature;
            testClassName = testClassName ?? string.Format(TestClassNameFormat, specFlowFeature.Name.ToIdentifier());
            var generationContext = CreateTestClassStructure(codeNamespace, testClassName, document);
            SetupTestClass(generationContext);
            SetupTestClassInitializeMethod(generationContext);
            SetupTestClassCleanupMethod(generationContext);
            SetupScenarioStartMethod(generationContext);
            SetupScenarioInitializeMethod(generationContext);
            _scenarioPartHelper.SetupFeatureBackground(generationContext);
            SetupScenarioCleanupMethod(generationContext);
            SetupTestInitializeMethod(generationContext);
            SetupTestCleanupMethod(generationContext);
            _unitTestMethodGenerator.CreateUnitTests(specFlowFeature, generationContext);
            _testGeneratorProvider.FinalizeTestClass(generationContext);
            return codeNamespace;
        }

        private TestClassGenerationContext CreateTestClassStructure(CodeNamespace codeNamespace, string testClassName, SpecFlowDocument document)
        {
            var codeTypeDeclaration = _codeDomHelper.CreateGeneratedTypeDeclaration(testClassName);
            codeNamespace.Types.Add(codeTypeDeclaration);
            return new TestClassGenerationContext(_testGeneratorProvider, document, codeNamespace, codeTypeDeclaration, DeclareTestRunnerMember(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), _codeDomHelper.CreateMethod(codeTypeDeclaration), document.SpecFlowFeature.HasFeatureBackground() ? _codeDomHelper.CreateMethod(codeTypeDeclaration) : null, _testGeneratorProvider.GetTraits().HasFlag(UnitTestGeneratorTraits.RowTests) && _specFlowConfiguration.AllowRowTests);
        }

        private CodeNamespace CreateNamespace(string targetNamespace)
        {
            targetNamespace = targetNamespace ?? "SpecFlowTests";
            if (!targetNamespace.StartsWith("global", StringComparison.CurrentCultureIgnoreCase) && _codeDomHelper.TargetLanguage == CodeDomProviderLanguage.VB)
            {
                targetNamespace = "GlobalVBNetNamespace." + targetNamespace;
            }

            return new CodeNamespace(targetNamespace)
            {
                Imports =
                {
                    new CodeNamespaceImport("TechTalk.SpecFlow"),
                    new CodeNamespaceImport("System"),
                    new CodeNamespaceImport("System.Linq"),
                    new CodeNamespaceImport("SpecFlow.Actions.Selenium")
                }
            };
        }

        private void SetupScenarioCleanupMethod(TestClassGenerationContext generationContext)
        {
            var scenarioCleanupMethod = generationContext.ScenarioCleanupMethod;
            scenarioCleanupMethod.Attributes = MemberAttributes.Public;
            scenarioCleanupMethod.Name = "ScenarioCleanup";
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            scenarioCleanupMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "CollectScenarioErrors"));
        }

        private void SetupTestClass(TestClassGenerationContext generationContext)
        {
            generationContext.TestClass.IsPartial = true;
            generationContext.TestClass.TypeAttributes |= TypeAttributes.Public;
            _linePragmaHandler.AddLinePragmaInitial(generationContext.TestClass, generationContext.Document.SourceFilePath);
            _testGeneratorProvider.SetTestClass(generationContext, generationContext.Feature.Name, generationContext.Feature.Description);
            _decoratorRegistry.DecorateTestClass(generationContext, out var unprocessedTags);
            if (unprocessedTags.Any())
            {
                _testGeneratorProvider.SetTestClassCategories(generationContext, unprocessedTags);
            }

            var codeMemberField = new CodeMemberField(typeof(string[]), "_featureTags");
            codeMemberField.InitExpression = _scenarioPartHelper.GetStringArrayExpression(generationContext.Feature.Tags);
            generationContext.TestClass.Members.Add(codeMemberField);
        }

        private CodeMemberField DeclareTestRunnerMember(CodeTypeDeclaration type)
        {
            var codeMemberField = new CodeMemberField(typeof(ITestRunner), "testRunner");
            type.Members.Add(codeMemberField);
            return codeMemberField;
        }

        private void SetupTestClassInitializeMethod(TestClassGenerationContext generationContext)
        {
            var testClassInitializeMethod = generationContext.TestClassInitializeMethod;
            testClassInitializeMethod.Attributes = MemberAttributes.Public;
            testClassInitializeMethod.Name = "FeatureSetup";
            _testGeneratorProvider.SetTestClassInitializeMethod(generationContext);
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            CodeExpression[] array2;
            if (!_testGeneratorProvider.GetTraits().HasFlag(UnitTestGeneratorTraits.ParallelExecution))
            {
                CodeExpression[] array = new CodePrimitiveExpression[2]
                {
                    new CodePrimitiveExpression(null),
                    new CodePrimitiveExpression(0)
                };
                array2 = array;
            }
            else
            {
                array2 = new CodeExpression[0];
            }

            var parameters = array2;
            testClassInitializeMethod.Statements.Add(new CodeAssignStatement(testRunnerExpression, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(TestRunnerManager)), "GetTestRunner", parameters)));
            testClassInitializeMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(FeatureInfo), "featureInfo", new CodeObjectCreateExpression(typeof(FeatureInfo), new CodeObjectCreateExpression(typeof(CultureInfo), new CodePrimitiveExpression(generationContext.Feature.Language)), new CodePrimitiveExpression(generationContext.Document.DocumentLocation?.FeatureFolderPath), new CodePrimitiveExpression(generationContext.Feature.Name), new CodePrimitiveExpression(generationContext.Feature.Description), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("ProgrammingLanguage"), _codeDomHelper.TargetLanguage.ToString()), _scenarioPartHelper.GetStringArrayExpression(generationContext.Feature.Tags))));
            testClassInitializeMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnFeatureStart", new CodeVariableReferenceExpression("featureInfo")));
        }

        private void SetupTestClassCleanupMethod(TestClassGenerationContext generationContext)
        {
            var testClassCleanupMethod = generationContext.TestClassCleanupMethod;
            testClassCleanupMethod.Attributes = MemberAttributes.Public;
            testClassCleanupMethod.Name = "FeatureTearDown";
            _testGeneratorProvider.SetTestClassCleanupMethod(generationContext);
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            testClassCleanupMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnFeatureEnd"));
            testClassCleanupMethod.Statements.Add(new CodeAssignStatement(testRunnerExpression, new CodePrimitiveExpression(null)));
        }

        private void SetupTestInitializeMethod(TestClassGenerationContext generationContext)
        {
            var testInitializeMethod = generationContext.TestInitializeMethod;
            testInitializeMethod.Attributes = MemberAttributes.Public;
            testInitializeMethod.Name = "TestInitialize";
            _testGeneratorProvider.SetTestInitializeMethod(generationContext);
        }

        private void SetupTestCleanupMethod(TestClassGenerationContext generationContext)
        {
            var testCleanupMethod = generationContext.TestCleanupMethod;
            testCleanupMethod.Attributes = MemberAttributes.Public;
            testCleanupMethod.Name = "TestTearDown";
            _testGeneratorProvider.SetTestCleanupMethod(generationContext);
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            testCleanupMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnScenarioEnd"));
        }

        internal virtual void SetupScenarioInitializeMethod(TestClassGenerationContext generationContext)
        {
            var scenarioInitializeMethod = generationContext.ScenarioInitializeMethod;
            scenarioInitializeMethod.Attributes = MemberAttributes.Public;
            scenarioInitializeMethod.Name = "ScenarioInitialize";
            scenarioInitializeMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(ScenarioInfo), "scenarioInfo"));
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            scenarioInitializeMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnScenarioInitialize", new CodeVariableReferenceExpression("scenarioInfo")));
        }

        private void SetupScenarioStartMethod(TestClassGenerationContext generationContext)
        {
            var scenarioStartMethod = generationContext.ScenarioStartMethod;
            scenarioStartMethod.Attributes = MemberAttributes.Public;
            scenarioStartMethod.Name = "ScenarioStart";
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            scenarioStartMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnScenarioStart"));
        }
    }
}