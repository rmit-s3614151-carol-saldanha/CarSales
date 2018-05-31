using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using OAuthExample.Utility;
using PagedList;
using Newtonsoft.Json;
using System.Text;
using System.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIClient
{
    public class HomeController : Controller
    {
        private Product _context;

        public static readonly HttpClient client = new HttpClient();

        //public async Task<IActionResult> Product(int page = 1, int pageSize = 5)
        //{

        //    List<Product> vdo = Program.repositories;
        //    PagedList<Product> model = new PagedList<Product>(vdo, page, pageSize);

        //    return View(model);
        //}

        public async Task<IActionResult> Product(int page = 1, int pageSize = 5)
        {

            client.BaseAddress = new Uri("http://sampledata.carsalesnetwork.com.au/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var values = JsonConvert.SerializeObject(new { Sort = "Make", PageNumber = 1, PageSize = 100 });
            var content = new StringContent(values, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/sample/listing", content);
            dynamic resultJson = await result.Content.ReadAsStringAsync();
            Console.WriteLine(JObject.Parse(resultJson)["Results"]);
            var serializer = new DataContractJsonSerializer(typeof(List<Product>));
            return View(JObject.Parse(resultJson)["Results"].ToObject<List<Product>>());

        }

        public async Task<IActionResult> Default()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Git Hub Users";
            return View();

        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    }
}