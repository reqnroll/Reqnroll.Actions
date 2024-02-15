using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Reqnroll.Actions.Configuration
{
    public interface ITargetIdentifier
    {
        List<string> GetAllAvailableTargets();
    }

    public class TargetIdentifier : ITargetIdentifier
    {
        private readonly IReqnrollActionJsonLocator _reqnrollActionJsonLocator;
        private TargetNameExtractor _targetNameExtractor;

        public TargetIdentifier(IReqnrollActionJsonLocator reqnrollActionJsonLocator)
        {
            _reqnrollActionJsonLocator = reqnrollActionJsonLocator;
            _targetNameExtractor = new TargetNameExtractor();
        }

        public List<string> GetAllAvailableTargets()
        {
            var reqnrollActionLocation = _reqnrollActionJsonLocator.GetFilePath();

            if (reqnrollActionLocation == null)
            {
                return new List<string>();
            }

            var reqnrollActionPath = Path.GetDirectoryName(reqnrollActionLocation);

            var targetConfigurations = Directory.GetFiles(reqnrollActionPath, "reqnroll.actions.*.json", SearchOption.TopDirectoryOnly);


            return targetConfigurations.Select(i => _targetNameExtractor.Extract(Path.GetFileName(i))).ToList();

        }
    }
}