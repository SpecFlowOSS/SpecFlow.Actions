using System;
using System.Runtime.ExceptionServices;

namespace SpecFlow.Actions.Configuration
{
    public class TargetNameExtractor
    {
        public string Extract(string targetFileName)
        {
            var splitted = targetFileName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);


            string targetName = "";
            for (int i = 2; i < splitted.Length-1; i++)
            {
                if (!string.IsNullOrWhiteSpace(targetName))
                {
                    targetName += ".";
                }

                targetName += splitted[i];
            }

            return targetName;
        }
    }
}