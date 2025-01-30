using System.Text.Json.Serialization;

namespace Actividad_Github_Cli.Clases
{
    public class CommitInfo
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}