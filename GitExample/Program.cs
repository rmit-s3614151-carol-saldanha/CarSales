using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Lecture6;

namespace WebAPIClient

{
    public static class Program
    {
        public static readonly HttpClient client = new HttpClient();

        public static List<GitUser> repositories = ProcessRepositories().Result; 
        public static void Main(string[] args)
        {            
            var repositories1 = ProcessRepositories().Result;

            foreach (var repo in repositories1)
                Console.WriteLine(repo.Description);
            
            var host = BuildWebHost(args);

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                //try
                //{
                //    SeedData.Initialize(services);
                //}
                //catch(Exception ex)
                //{
                //    var logger = services.GetRequiredService<ILogger<Program>>();
                //    logger.LogError(ex, "An error occurred seeding the DB.");
                //}
            }

            BuildWebHost(args).Run();
        }

        private static async Task<List<GitUser>> ProcessRepositories()
        {
            //public List<GitUser> desc = new List<GitUser>();
            var serializer = new DataContractJsonSerializer(typeof(List<GitUser>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<GitUser>;

            Console.WriteLine("Hello");
            // Console.WriteLine(GitUser);
            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Description);
                Console.WriteLine(repo.GitHubHomeUrl);

            }
            Console.WriteLine();
            
           
            return repositories;

           
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
