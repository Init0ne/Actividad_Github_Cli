﻿using Actividad_Github_Cli.Clases;
using System.Text.Json;

namespace Actividad_Github_Cli
{
    class Program
    {
        private static readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

        static async Task Main(string[] args)
        {
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
                    List<GitHubEvent>? events = JsonSerializer.Deserialize<List<GitHubEvent>>(json, jsonOptions);

                    Console.WriteLine($"\nActividad reciente de {username}:");
                    Console.WriteLine("===============================================\n");

                    for (int eventIndex = 0; eventIndex < events?.Count; eventIndex++)
                    {
                        GitHubEvent evento = events[eventIndex];
                        string repoName = evento.Repo?.Name?.Split('/')[1] ?? "repo desconocido";
                        string mensaje = evento.Type switch
                        {
                            "PushEvent" => $"+ Hizo push de {evento.Payload?.Commits?.Count ?? 0} commit(s) en {repoName}",
                            "WatchEvent" => $"* Dio una estrella a {repoName}",
                            "IssuesEvent" => $"- {(evento.Payload?.Action == "opened" ? "Abrió" : "Modificó")} un issue en {repoName}",
                            _ => $"> Actividad de tipo {evento.Type} en {repoName}"
                        };

                        Console.WriteLine($"{mensaje} ({evento.CreatedAt:dd/MM/yyyy HH:mm})");
                    }

                    Console.WriteLine("\n===============================================");
                }
                else
                {
                    Console.WriteLine($"\nError: {(int)response.StatusCode} {response.ReasonPhrase}");
                    Console.WriteLine($"\nWTF!  Quien es {username}?\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("\n¿Probaste reiniciar la matrix? (o Visual Studio)\n");
            }
        }
    }
}