using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Reqnroll.Actions.Android.Configuration
{
    public class AndroidJsonPart
    {
        [JsonInclude] public Dictionary<string, string> Capabilities { get; set; } = new();

        [JsonInclude] public int? Timeout { get; set; }
    }
}
