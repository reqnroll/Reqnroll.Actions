using System.Text.Json.Serialization;

namespace Reqnroll.Actions.WindowsAppDriver.Configuration
{
    public class ReqnrollActionJson
    {
        [JsonInclude]
        public WindowsAppDriverJsonPart WindowsAppDriver { get; private set; } = new WindowsAppDriverJsonPart();
    }
}
