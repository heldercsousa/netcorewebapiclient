using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPIClient;
using System.Diagnostics;

namespace WebApiClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                Console.WriteLine("LENDO22222!!!!!!");
                var repositories = await ProcessRepositories();
                foreach (var repo in repositories)
                {
                    Console.WriteLine(repo.Name);
                    Console.WriteLine(repo.Description);
                    Console.WriteLine(repo.GitHubHomeUrl);
                    Console.WriteLine(repo.Homepage);
                    Console.WriteLine(repo.Watchers);
                     Console.WriteLine(repo.LastPush);
                    Console.WriteLine();
                }
            });
            Console.WriteLine("LENDO!!!!!!");
            Console.Read();
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

            // foreach (var repo in repositories)
            //     Console.Write(repo.Name + Environment.NewLine);

            return repositories;

        }
    }
}
