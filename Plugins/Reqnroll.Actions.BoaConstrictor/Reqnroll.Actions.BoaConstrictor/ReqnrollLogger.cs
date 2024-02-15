// unset

using Boa.Constrictor.Logging;
using Reqnroll.Infrastructure;

namespace Reqnroll.Actions.BoaConstrictor
{
    public class ReqnrollLogger : AbstractLogger
    {
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;

        public ReqnrollLogger(IReqnrollOutputHelper reqnrollOutputHelper)
        {
            _reqnrollOutputHelper = reqnrollOutputHelper;
        }

        public override void Close()
        {
            
        }

        protected override void LogRaw(string message, LogSeverity severity = LogSeverity.Info)
        {
            _reqnrollOutputHelper.WriteLine($"[{severity.ToString()}] {message}");
        }
    }
}