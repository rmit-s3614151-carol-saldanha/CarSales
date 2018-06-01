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
using CarSales.Utility;
using PagedList;
using Newtonsoft.Json;
using System.Text;
using System.Json;
using Newtonsoft.Json.Linq;
using System.Web;
namespace WebAPIClient
{
    public class HomeController : Controller
    {
        //Using HttpClient from Statrup.cs to make requests to the API
        public static readonly HttpClient client = Startup.client;

        //Declaring page size and page for pagination
        public const int pageSize = 5;
        public const int Page = 1;


        /*This method loads when the page is first loaded and displays 5 products per page
         * with sorting based on when the user clicks the Price column of the table. */

        public async Task<IActionResult> Product(string sortOrder, string currentFilter,string searchString, int? page)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm1"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["NameSortParm2"] = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";

            if (searchString != null)
            {
                page = Page;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

       
            var values = JsonConvert.SerializeObject(new { Sort = "Make", PageNumber = 1, PageSize = 100 });
            var content = new StringContent(values, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/sample/listing", content);
            var resultJson = await result.Content.ReadAsStringAsync();
            Console.WriteLine(JObject.Parse(resultJson)["Results"]);

            var query = JObject.Parse(resultJson)["Results"].ToObject<List<Product>>();

            //Sort based on what the user clicks
            switch (sortOrder)
            {
                case "name_desc":

                    query = query.OrderByDescending(s => s.Price).ToList();
                    break;
                case "name_asc":
                    query = query.OrderBy(s => s.Price).ToList();
                    break;

                default:
                    query = JObject.Parse(resultJson)["Results"].ToObject<List<Product>>();
                    break;
            }

            return View(await PaginatedList<Product>
                        .CreateAsync(query, page ?? 1, pageSize));

        }

        //About Page
        public IActionResult About()
        {
            ViewData["Message"] = "Car Sales";
            return View();

        }

        //Contact page
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    }
}
