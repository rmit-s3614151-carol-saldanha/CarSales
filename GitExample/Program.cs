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

        //Git User name which is searched
        public static string user="";

        //List of first 30 user repositories
        public static List<GitUser> repositories = ProcessRepositories().Result;

        public static void Main(string[] args)
        {
           
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

            }

            BuildWebHost(args).Run();
        }

        //This method is used to make Git API calls to get the first 30 users

        private static async Task<List<GitUser>> ProcessRepositories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<GitUser>));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "rmit-s3614151-carol-saldanha");
            var streamTask = client.GetStreamAsync("https://api.github.com/users?since=0");
            var repositories = serializer.ReadObject(await streamTask) as List<GitUser>;
            return repositories;
        }

        //This method is used to get a list of repositories of the searched user.

        public static async Task<List<SearchUser>> search()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<SearchUser>));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "rmit-s3614151-carol-saldanha");
            var url = "https://api.github.com/users/";
            var searchUrl = Combine(url, user);
            var streamTask = client.GetStreamAsync(searchUrl);
            var searchResult = serializer.ReadObject(await streamTask) as List<SearchUser>;
            return searchResult;

        }

        //Get the user name that is searched
        public static string getUserName(string userName)
        {
            user = userName;
            return user;
        }

        //Concatenate the searched user name to the API url 
        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            string uri3 = "repos".TrimStart('/');
            return string.Format("{0}/{1}/{2}", uri1, uri2, uri3);
        }


        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}