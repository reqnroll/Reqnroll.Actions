using System;
using System.IO;

namespace Reqnroll.Actions.Configuration
{
    public class ReqnrollActionJsonLocator : IReqnrollActionJsonLocator
    {
        public const string JsonConfigurationFileName = "reqnroll.actions.json";

        public string? GetFilePath()
        {
            return GetFilePathToConfigurationFile(JsonConfigurationFileName);
        }

        public string? GetTargetFilePath(string targetName)
        {
            return GetFilePathToConfigurationFile($"reqnroll.actions.{targetName}.json");
        }

        private string? GetFilePathToConfigurationFile(string configurationFileName)
        {
            if (AppDomain.CurrentDomain.BaseDirectory is not null)
            {
                var reqnrollJsonFileInAppDomainBaseDirectory =
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFileName);

                if (File.Exists(reqnrollJsonFileInAppDomainBaseDirectory))
                {
                    return reqnrollJsonFileInAppDomainBaseDirectory;
                }

                var reqnrollJsonFileTwoDirectoriesUp =
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", configurationFileName);

                if (File.Exists(reqnrollJsonFileTwoDirectoriesUp))
                {
                    return reqnrollJsonFileTwoDirectoriesUp;
                }
            }

            var reqnrollJsonFileInCurrentDirectory = Path.Combine(Environment.CurrentDirectory, configurationFileName);

            if (File.Exists(reqnrollJsonFileInCurrentDirectory))
            {
                return reqnrollJsonFileInCurrentDirectory;
            }

            return null;
        }
    }
}