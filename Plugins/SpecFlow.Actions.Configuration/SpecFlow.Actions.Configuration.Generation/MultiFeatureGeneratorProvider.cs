using BoDi;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator.CodeDom;
using TechTalk.SpecFlow.Generator.Generation;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Parser;

namespace SpecFlow.Actions.Configuration.Generation
{
    internal class MultiFeatureGeneratorProvider : IFeatureGeneratorProvider
    {
        private readonly ITargetIdentifier _targetIdentifier;
        private readonly MultiFeatureGenerator _multiFeatureGenerator;

        public MultiFeatureGeneratorProvider(IObjectContainer container, ITargetIdentifier targetIdentifier)
        {
            _targetIdentifier = targetIdentifier;
            var featureGenerators = new List<IFeatureGenerator>();


            var targets = _targetIdentifier.GetAllAvailableTargets();

            if (targets.Any())
            {
                foreach (var target in targets)
                {
                    var targetFeatureGenerator = new UnitTestTargetFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<SpecFlowConfiguration>(), container.Resolve<IDecoratorRegistry>(), target);
                    featureGenerators.Add(targetFeatureGenerator);
                }
            }
            else
            {
                featureGenerators.Add(container.Resolve<UnitTestFeatureGenerator>());
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