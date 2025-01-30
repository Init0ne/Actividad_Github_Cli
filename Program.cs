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
            args = ["Init0ne"]; // Hardcoded string for testing
            if (args.Length == 0)
            {
                Console.WriteLine("Debes ingresar un nombre de usuario de GitHub");
                Console.WriteLine("Ejemplo: GithubActivity Init0ne");
                return;
            }

            string username = args[0];
            string url = $"https://api.github.com/users/{username}/events";

            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", "GithubActivityCLI");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
                    List<GitHubEvent>? events = JsonSerializer.Deserialize<List<GitHubEvent>>(json, options);

                    Console.WriteLine($"Actividad reciente de {username}:");
                    Console.WriteLine("====================================");

                    foreach (GitHubEvent evento in events)
                    {
                        string repoName = evento.Repo?.Name?.Split('/')[1] ?? "repo desconocido";
                        string mensaje = evento.Type switch
                        {
                            "PushEvent" => $"> Hizo push de {evento.Payload?.Commits?.Count ?? 0} commit(s) en {repoName}",
                            "WatchEvent" => $"* Dio una estrella a {repoName}",
                            "IssuesEvent" => $"- {(evento.Payload?.Action == "opened" ? "Abrió" : "Modificó")} un issue en {repoName}",
                            _ => $"- Actividad de tipo {evento.Type} en {repoName}"
                        };

                        Console.WriteLine($"{mensaje} ({evento.CreatedAt:dd/MM/yyyy HH:mm})");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {(int)response.StatusCode} {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}