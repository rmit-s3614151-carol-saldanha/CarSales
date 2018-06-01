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

using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WebAPIClient

{
    public static class Program
    {
       
  
        //Git User name which is searched
        public static string user="";

        //List of first 30 user repositories
       

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
     


        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}