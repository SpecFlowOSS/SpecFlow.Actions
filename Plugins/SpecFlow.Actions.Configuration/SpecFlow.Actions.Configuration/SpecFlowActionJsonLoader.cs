using System.IO;

namespace SpecFlow.Actions.Configuration
{
    public class SpecFlowActionJsonLoader : ISpecFlowActionJsonLoader
    {
        private readonly ISpecFlowActionJsonLocator _specFlowActionJsonLocator;
        private readonly CurrentTargetIdentifier _currentTargetIdentifier;

        public SpecFlowActionJsonLoader(ISpecFlowActionJsonLocator specFlowActionJsonLocator, CurrentTargetIdentifier currentTargetIdentifier)
        {
            _specFlowActionJsonLocator = specFlowActionJsonLocator;
            _currentTargetIdentifier = currentTargetIdentifier;
        }

        public string Load()
        {
            var specFlowJsonFilePath = _specFlowActionJsonLocator.GetFilePath();

            if (specFlowJsonFilePath != null)
            {
                var content = File.ReadAllText(specFlowJsonFilePath);

                return content;
            }

            return "{}";
        }

        public string LoadTarget()
        {
            if (_currentTargetIdentifier.Name != null)
            {
                var specFlowJsonFilePath = _specFlowActionJsonLocator.GetTargetFilePath(_currentTargetIdentifier.Name);

                if (specFlowJsonFilePath != null)
                {
                    var content = File.ReadAllText(specFlowJsonFilePath);

                    return content;
                }
            }

            return "{}";
        }
    }
}