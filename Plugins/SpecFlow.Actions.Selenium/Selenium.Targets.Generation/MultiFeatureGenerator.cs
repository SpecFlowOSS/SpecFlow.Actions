using SpecFlow.Actions.Selenium;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TechTalk.SpecFlow.Generator.Generation;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Parser;

namespace Selenium.Targets.Generation
{
    internal class MultiFeatureGenerator : IFeatureGenerator
    {
        private readonly KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>[] _featureGenerators;

        public MultiFeatureGenerator(IEnumerable<KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>> featureGenerators)
        {
            _featureGenerators = featureGenerators.ToArray();

            foreach (var featureGenerator in _featureGenerators)
            {
                if (featureGenerator.Value is UnitTestFeatureGenerator unitTestFeatureGenerator)
                {
                    unitTestFeatureGenerator.TestClassNameFormat += $"_{featureGenerator.Key.Browser}";
                }
            }
        }

        public CodeNamespace GenerateUnitTestFixture(SpecFlowDocument specFlowDocument, string testClassName, string targetNamespace)
        {
            CodeNamespace? result = null;

            foreach (var featureGenerator in _featureGenerators)
            {
                var featureGeneratorResult = featureGenerator.Value.GenerateUnitTestFixture(specFlowDocument, testClassName, targetNamespace);

                if (result == null)
                {
                    result = featureGeneratorResult;
                }
                else
                {
                    foreach (CodeTypeDeclaration type in featureGeneratorResult.Types)
                    {
                        result.Types.Add(type);
                    }
                }
            }

            if (result == null)
            {
                result = new CodeNamespace(targetNamespace);
            }

            return result;
        }
    }
}