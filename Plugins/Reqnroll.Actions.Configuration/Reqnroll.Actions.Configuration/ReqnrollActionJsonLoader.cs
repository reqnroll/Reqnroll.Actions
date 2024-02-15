using System.IO;

namespace Reqnroll.Actions.Configuration
{
    public class ReqnrollActionJsonLoader : IReqnrollActionJsonLoader
    {
        private readonly IReqnrollActionJsonLocator _reqnrollActionJsonLocator;
        private readonly CurrentTargetIdentifier _currentTargetIdentifier;

        public ReqnrollActionJsonLoader(IReqnrollActionJsonLocator reqnrollActionJsonLocator, CurrentTargetIdentifier currentTargetIdentifier)
        {
            _reqnrollActionJsonLocator = reqnrollActionJsonLocator;
            _currentTargetIdentifier = currentTargetIdentifier;
        }

        public string Load()
        {
            var reqnrollJsonFilePath = _reqnrollActionJsonLocator.GetFilePath();

            if (reqnrollJsonFilePath != null)
            {
                var content = File.ReadAllText(reqnrollJsonFilePath);

                return content;
            }

            return "{}";
        }

        public string LoadTarget()
        {
            if (_currentTargetIdentifier.Name != null)
            {
                var reqnrollJsonFilePath = _reqnrollActionJsonLocator.GetTargetFilePath(_currentTargetIdentifier.Name);

                if (reqnrollJsonFilePath != null)
                {
                    var content = File.ReadAllText(reqnrollJsonFilePath);

                    return content;
                }
            }

            return "{}";
        }
    }
}