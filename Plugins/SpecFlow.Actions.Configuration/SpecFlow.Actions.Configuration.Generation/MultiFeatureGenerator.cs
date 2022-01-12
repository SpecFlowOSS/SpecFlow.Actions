using System.CodeDom;
using System.Collections.Generic;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Parser;

namespace Selenium.Targets.Generation
{
    internal class MultiFeatureGenerator : IFeatureGenerator
    {
        private readonly List<IFeatureGenerator> _featureGenerators;

        public MultiFeatureGenerator(List<IFeatureGenerator> featureGenerators)
        {
            _featureGenerators = featureGenerators;
        }

        public CodeNamespace GenerateUnitTestFixture(SpecFlowDocument specFlowDocument, string testClassName, string targetNamespace)
        {
            CodeNamespace? result = null;

            foreach (var featureGenerator in _featureGenerators)
            {
                var featureGeneratorResult = featureGenerator.GenerateUnitTestFixture(specFlowDocument, testClassName, targetNamespace);

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