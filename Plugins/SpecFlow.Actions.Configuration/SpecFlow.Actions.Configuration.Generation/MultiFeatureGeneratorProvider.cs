using BoDi;
using System.Collections.Generic;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator.CodeDom;
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
            var featureGenerators = new List<IFeatureGenerator>();

            //foreach (var seleniumSpecFlowJson in seleniumTargetsConfiguration.Targets.Selenium)
            //{
                var targetFeatureGenerator = new UnitTestTargetFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<SpecFlowConfiguration>(), container.Resolve<IDecoratorRegistry>());
                featureGenerators.Add(targetFeatureGenerator);
            //}

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