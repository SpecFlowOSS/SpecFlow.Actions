using System.IO;
using TechTalk.SpecFlow.Configuration;

namespace SpecFlow.Actions.Selenium
{

    public interface ISpecFlowJsonLoader
    {
        string Load();
    }
    public class SpecFlowJsonLoader : ISpecFlowJsonLoader
    {
        private readonly ISpecFlowJsonLocator _specFlowJsonLocator;

        public SpecFlowJsonLoader(ISpecFlowJsonLocator specFlowJsonLocator)
        {
            _specFlowJsonLocator = specFlowJsonLocator;
        }

        public string Load()
        {
            var specFlowJsonFilePath = _specFlowJsonLocator.GetSpecFlowJsonFilePath();
            var specflowJsonContent = File.ReadAllText(specFlowJsonFilePath);


            return specflowJsonContent;
        }
    }
}