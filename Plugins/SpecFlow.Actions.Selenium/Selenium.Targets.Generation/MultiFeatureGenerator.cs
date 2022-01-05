using SpecFlow.Actions.Selenium;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TechTalk.SpecFlow.Generator.Generation;
using TechTalk.SpecFlow.Generator.UnitTestConverter;
using TechTalk.SpecFlow.Parser;

namespace Selenium.Targets.Generation
{
    internal class MultiFeatureGenerator : IFeatureGenerator
    {
        private readonly IFeatureGenerator _defaultFeatureGenerator;

        private readonly KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>[] _featureGenerators;

        private readonly List<string> _unitTestProviderTags = new List<string> { "xunit", "mstest", "nunit3" };

        public MultiFeatureGenerator(IEnumerable<KeyValuePair<SeleniumSpecFlowJsonPart, IFeatureGenerator>> featureGenerators, IFeatureGenerator defaultFeatureGenerator)
        {
            _defaultFeatureGenerator = defaultFeatureGenerator;
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
            bool onlyFullframework = false;

            var specFlowFeature = specFlowDocument.SpecFlowFeature;
            bool onlyDotNetCore = false;
            if (specFlowFeature.HasTags())
            {
                if (specFlowFeature.Tags.Where(t => t.Name == "@SingleTestConfiguration").Any())
                {
                    return _defaultFeatureGenerator.GenerateUnitTestFixture(specFlowDocument, testClassName, targetNamespace);
                }

                onlyFullframework = HasFeatureTag(specFlowFeature, "@fullframework");
                onlyDotNetCore = HasFeatureTag(specFlowFeature, "@dotnetcore");
            }

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                onlyFullframework = false;
                onlyDotNetCore = true;
            }

            var tagsOfFeature = specFlowFeature.Tags.Select(t => t.Name);
            var unitTestProviders = tagsOfFeature.Where(t => _unitTestProviderTags.Where(utpt => string.Compare(t, "@" + utpt, StringComparison.CurrentCultureIgnoreCase) == 0).Any());

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

        private bool HasFeatureTag(SpecFlowFeature specFlowFeature, string tag)
        {
            return specFlowFeature.Tags.Any(t => string.Compare(t.Name, tag, StringComparison.CurrentCultureIgnoreCase) == 0);
        }
    }
}