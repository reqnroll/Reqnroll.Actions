using System.Text.Json.Serialization;

namespace Reqnroll.Actions.Browserstack
{

    internal class BrowserstackExecutor
    {
        [JsonPropertyName("action")] public string? Action { get; set; }

        [JsonPropertyName("arguments")] public Arguments? Arguments { get; set; }
    }
}