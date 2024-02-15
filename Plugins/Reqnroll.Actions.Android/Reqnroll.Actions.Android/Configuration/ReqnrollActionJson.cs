using System.Text.Json.Serialization;

namespace Reqnroll.Actions.Android.Configuration
{
    public class ReqnrollActionJson
    {
        [JsonInclude] public AndroidJsonPart Android { get; private set; } = new AndroidJsonPart();

        [JsonInclude] public AppiumServerJsonPart AppiumServer { get; private set; } = new AppiumServerJsonPart();
    }
}
