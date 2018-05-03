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
           

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
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
            var serializer = new DataContractJsonSerializer(typeof(List<GitUser>));

            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            //var byteArray = En÷coding.ASCII.GetBytes("rmit-s3614151-carol-saldanha:Car@2612");
            client.DefaultRequestHeaders.Add("User-Agent", "rmit-s3614151-carol-saldanha");

            //client.DefaultRequestHeaders.Add("Basic", Convert.ToBase64String(byteArray))));


            var streamTask = client.GetStreamAsync("https://api.github.com/users");
            var repositories = serializer.ReadObject(await streamTask) as List<GitUser>;

            Console.WriteLine("Hello");
            // Console.WriteLine(GitUser);
            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.id);
                Console.WriteLine(repo.login);

            }
            Console.WriteLine();


            return repositories;


        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}