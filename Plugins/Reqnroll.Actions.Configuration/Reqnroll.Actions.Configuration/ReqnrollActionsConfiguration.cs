using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Reqnroll.Actions.Configuration
{
    public interface IReqnrollActionsConfiguration
    {
        string Get(string path);
        string Get(string path, string defaultValue);
        double? GetDouble(string path);
        string[] GetArray(string path);
        Dictionary<string, string> GetDictionary(string path);
    }

    public class ReqnrollActionsConfiguration : IReqnrollActionsConfiguration
    {
        private readonly IReqnrollActionJsonLoader _reqnrollActionJsonLoader;
        private readonly Lazy<IConfigurationRoot> _configuration;


        public ReqnrollActionsConfiguration(IReqnrollActionJsonLoader reqnrollActionJsonLoader)
        {
            _reqnrollActionJsonLoader = reqnrollActionJsonLoader;

            _configuration = new Lazy<IConfigurationRoot>(LoadConfiguration);
        }

        private IConfigurationRoot LoadConfiguration()
        {
            var reqnrollActionJsonContent = _reqnrollActionJsonLoader.Load();
            var reqnrollActionTargetJsonContent = _reqnrollActionJsonLoader.LoadTarget();

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(reqnrollActionJsonContent)));
            configurationBuilder = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(reqnrollActionTargetJsonContent)));
            return configurationBuilder.Build();
        }

        public string Get(string path)
        {
            var configValue = _configuration.Value[path];
            return configValue;
        }

        public double? GetDouble(string path)
        {
            var configValue = _configuration.Value[path];
            if (configValue != null)
            {
                return Convert.ToDouble(configValue);
            }

            return null;
        }

        public string Get(string path, string defaultValue)
        {
            var configValue = _configuration.Value[path];

            if (configValue is null)
            {
                return defaultValue;
            }

            return configValue;
        }

        public string[] GetArray(string path)
        {
            return _configuration.Value.GetSection(path).Get<string[]>();
        }

        public Dictionary<string, string> GetDictionary(string path)
        {
            var configurationSection = _configuration.Value.GetSection(path);

            if (configurationSection is null)
            {
                return new Dictionary<string, string>();
            }

            var dictionary = new Dictionary<string, string>();
            foreach (var child in configurationSection.GetChildren())
            {
                dictionary[child.Key] = child.Value;
            }

            return dictionary;
        }
    }
}