using System.Text.Json.Serialization;

namespace Actividad_Github_Cli.Clases
{
    public class GitHubEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("repo")]
        public RepoInfo Repo { get; set; }

        [JsonPropertyName("payload")]
        public PayloadInfo Payload { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}