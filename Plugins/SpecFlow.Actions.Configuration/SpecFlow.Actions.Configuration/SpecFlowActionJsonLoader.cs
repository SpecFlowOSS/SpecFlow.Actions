using System.IO;

namespace SpecFlow.Actions.Configuration
{
    public class SpecFlowActionJsonLoader : ISpecFlowActionJsonLoader
    {
        private readonly ISpecFlowActionJsonLocator _specFlowActionJsonLocator;

        public SpecFlowActionJsonLoader(ISpecFlowActionJsonLocator specFlowActionJsonLocator)
        {
            _specFlowActionJsonLocator = specFlowActionJsonLocator;
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
    }
}