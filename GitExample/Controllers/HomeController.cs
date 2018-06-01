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
        public Product product = new Product();




        public static readonly HttpClient client = Startup.client;
        //public async Task<IActionResult> Product(int page = 1, int pageSize = 5)
        //{

        //    List<Product> vdo = Program.repositories;
        //    PagedList<Product> model = new PagedList<Product>(vdo, page, pageSize);

        //    return View(model);
        //}

        public async Task<IActionResult> Product(string sortOrder, string currentFilter,string searchString, int? page)
        {


            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //ViewData["StoreSortParm"] = sortOrder == "Store" ? "store_desc" : "Store";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            //var query

            //if (!string.IsNullOrWhiteSpace(searchString))
            //{
            //    query = query.Where(x => x.Product.Name.Contains(searchString));

            //}

   
            int pageSize = 5;
            
            var values = JsonConvert.SerializeObject(new { Sort = "Make", PageNumber = 1, PageSize = 100 });
            var content = new StringContent(values, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("/sample/listing", content);
            var resultJson = await result.Content.ReadAsStringAsync();
            Console.WriteLine(JObject.Parse(resultJson)["Results"]);
            //var serializer = new DataContractJsonSerializer(typeof(List<Product>));

            var query = JObject.Parse(resultJson)["Results"].ToObject<List<Product>>();


            //var query1 = query[0].Make;

           

            switch (sortOrder)
            {
                case "name_desc":

                    //query = query.OrderByDescending(s => s.Price);

                   
                    var descListOb = query.OrderBy(x => x.Price);
                    
        
               
                    break;



                default:
                    Console.WriteLine();
                    break;
            }


            return View(await PaginatedList<Product>
                   .CreateAsync(JObject.Parse(resultJson)["Results"].ToObject<List<Product>>(), page ?? 1, pageSize));

        }

       

        public async Task<IActionResult> Default()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Car Sales";
            return View();

        }

        [HttpPost]
        public ActionResult Index(Product model)
        {

            return View("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    }
}
