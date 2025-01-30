using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Actividad_Github_Cli
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

    public class RepoInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class PayloadInfo
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("commits")]
        public List<CommitInfo> Commits { get; set; }
    }

    public class CommitInfo
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            #region HTTP client

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "GithubActivityCLI");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            #endregion

            #region Url builder

            string username = args[0];
            string apiUrl = $"https://api.github.com/users/{Uri.EscapeDataString(username)}/events";

            #endregion

            try
            {
                HttpResponseMessage respuesta = await client.GetAsync($"https://api.github.com/users/{username}/events");

                if (respuesta.IsSuccessStatusCode)
                {
                    string json = await respuesta.Content.ReadAsStringAsync();
                    List<GitHubEvent>? eventos = JsonSerializer.Deserialize<List<GitHubEvent>>(json);

                    foreach (GitHubEvent evento in eventos)
                    {
                        Console.WriteLine($"{evento.Type} en {evento.Repo?.Name ?? "repo desconocido"}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {respuesta.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡Ups! Algo falló: {ex.Message}");
            }
        }
    }
}