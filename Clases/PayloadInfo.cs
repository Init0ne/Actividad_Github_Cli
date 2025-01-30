using System.Text.Json.Serialization;

namespace Actividad_Github_Cli.Clases
{
    public class PayloadInfo
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("commits")]
        public List<CommitInfo>? Commits { get; set; }
    }
}