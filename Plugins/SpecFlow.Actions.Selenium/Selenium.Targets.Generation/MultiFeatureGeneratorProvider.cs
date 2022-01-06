using BoDi;
using SpecFlow.Actions.Selenium;
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

        public MultiFeatureGeneratorProvider(IObjectContainer container, SeleniumTargetsConfiguration seleniumTargetsConfiguration)
        {
            var featureGenerators = new List<KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>>();

            foreach (var seleniumSpecFlowJson in seleniumTargetsConfiguration.Targets.Selenium)
            {
                var combinationFeatureGenerator = new UnitTestFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<SpecFlowConfiguration>(), container.Resolve<IDecoratorRegistry>());
                featureGenerators.Add(new KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>(seleniumSpecFlowJson, combinationFeatureGenerator));
            }

            _multiFeatureGenerator = new MultiFeatureGenerator(featureGenerators);
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