using System;
using System.IO;

namespace SpecFlow.Actions.Configuration
{
    public class SpecFlowActionJsonLocator : ISpecFlowActionJsonLocator
    {
        public const string JsonConfigurationFileName = "specflow.actions.json";

        public string? GetFilePath()
        {
            return GetFilePathToConfigurationFile(JsonConfigurationFileName);
        }

        public string? GetTargetFilePath(string? targetName)
        {
            return GetFilePathToConfigurationFile($"specflow.actions.{targetName}.json");
        }

        private string? GetFilePathToConfigurationFile(string configurationFileName)
        {
            var specflowJsonFileInAppDomainBaseDirectory =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFileName);

            if (File.Exists(specflowJsonFileInAppDomainBaseDirectory))
            {
                return specflowJsonFileInAppDomainBaseDirectory;
            }

            var specflowJsonFileTwoDirectoriesUp =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", configurationFileName);

            if (File.Exists(specflowJsonFileTwoDirectoriesUp))
            {
                return specflowJsonFileTwoDirectoriesUp;
            }

            var specflowJsonFileInCurrentDirectory = Path.Combine(Environment.CurrentDirectory, configurationFileName);

            if (File.Exists(specflowJsonFileInCurrentDirectory))
            {
                return specflowJsonFileInCurrentDirectory;
            }

            return null;
        }
    }
}