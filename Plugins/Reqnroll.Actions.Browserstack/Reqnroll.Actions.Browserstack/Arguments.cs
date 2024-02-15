using System.Text.Json.Serialization;

namespace Reqnroll.Actions.Browserstack
{

    internal class Arguments
    {
        [JsonPropertyName("status")] public string? Status { get; set; }

        [JsonPropertyName("reason")] public string? Reason { get; set; }
    }
}