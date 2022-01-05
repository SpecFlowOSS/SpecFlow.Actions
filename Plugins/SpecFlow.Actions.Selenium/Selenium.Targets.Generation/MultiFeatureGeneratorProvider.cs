using BoDi;
using System.Collections.Generic;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator.CodeDom;
using TechTalk.SpecFlow.Generator.Generation;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Parser;

namespace Selenium.Targets.Generation
{
    internal class MultiFeatureGeneratorProvider : IFeatureGeneratorProvider
    {
        private readonly MultiFeatureGenerator _multiFeatureGenerator;

        public MultiFeatureGeneratorProvider(IObjectContainer container)
        {
            var list = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                list.Add($"Generated_Feature_{i}");
            }

            var featureGenerators = new List<KeyValuePair<string, IFeatureGenerator>>();

            foreach (var combination in list)
            {
                var combinationFeatureGenerator = new UnitTestFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<SpecFlowConfiguration>(), container.Resolve<IDecoratorRegistry>());
                featureGenerators.Add(new KeyValuePair<string, IFeatureGenerator>(combination, combinationFeatureGenerator));
            }

            _multiFeatureGenerator = new MultiFeatureGenerator(featureGenerators, new UnitTestFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<SpecFlowConfiguration>(), container.Resolve<IDecoratorRegistry>()));
        }

        public bool CanGenerate(SpecFlowDocument specFlowDocument)
        {
            return true;
        }

        public IFeatureGenerator CreateGenerator(SpecFlowDocument specFlowDocument)
        {
            return _multiFeatureGenerator;
        }

        public int Priority => PriorityValues.Normal;
    }
}