using System.Text.Json.Serialization;

namespace Actividad_Github_Cli.Clases
{
    public class RepoInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}