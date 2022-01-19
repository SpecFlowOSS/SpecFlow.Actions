using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpecFlow.Actions.Configuration
{
    public interface ITargetIdentifier
    {
        List<string> GetAllAvailableTargets();
    }

    public class TargetIdentifier : ITargetIdentifier
    {
        private readonly ISpecFlowActionJsonLocator _specFlowActionJsonLocator;
        private TargetNameExtractor _targetNameExtractor;

        public TargetIdentifier(ISpecFlowActionJsonLocator specFlowActionJsonLocator)
        {
            _specFlowActionJsonLocator = specFlowActionJsonLocator;
            _targetNameExtractor = new TargetNameExtractor();
        }

        public List<string> GetAllAvailableTargets()
        {
            var specflowActionLocation = _specFlowActionJsonLocator.GetFilePath();

            if (specflowActionLocation == null)
            {
                return new List<string>();
            }

            var specflowActionPath = Path.GetDirectoryName(specflowActionLocation);

            var targetConfigurations = Directory.GetFiles(specflowActionPath, "specflow.actions.*.json", SearchOption.TopDirectoryOnly);


            return targetConfigurations.Select(i => _targetNameExtractor.Extract(Path.GetFileName(i))).ToList();

        }
    }
}