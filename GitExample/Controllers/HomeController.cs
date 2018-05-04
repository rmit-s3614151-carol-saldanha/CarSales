using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIClient;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace WebAPIClient
{
    public class HomeController : Controller
    {

     

        // Auto-parsed variables coming in from the request - there is a form on the page to send this data.
        //public async Task<IActionResult> Index(string productName)
        //{
        //    // Eager loading the Product table - join between OwnerInventory and the Product table.
        //    var query = _context.OwnerInventory.Include(x => x.Product).Select(x => x);

        //    if (!string.IsNullOrWhiteSpace(productName))
        //    {
        //        // Adding a where to the query to filter the data.
        //        // Note for the first request productName is null thus the where is not always added.
        //        query = query.Where(x => x.Product.Name.Contains(productName));

        //        // Storing the search into ViewBag to populate the textbox with the same value for convenience.
        //        ViewBag.ProductName = productName;
        //    }

        //    // Adding an order by to the query for the Product name.
        //    query = query.OrderBy(x => x.Product.Name);

        //    // Passing a List<OwnerInventory> model object to the View.
        //    return View(await query.ToListAsync());
        //}

        public async Task<IActionResult> Git(string userName)
        {

         
            return View( Program.repositories);
        }

        public async Task<IActionResult> Search(string userName)
        {
            
            Regex regex = new Regex(@"/\B@(?!.*(-){2,}.*)[a-z0-9](?:[a-z0-9-]{0,37}[a-z0-9])?\b/ig");
            Match match = regex.Match(userName);
            if (!match.Success)
            {
                return View("~/Views/Home/Page404.cshtml");
            }
            else if (!string.IsNullOrWhiteSpace(userName))
            {
                Program.getUserName(userName);
                ViewBag.userName = userName;
                return View(await Program.search());
            }
          
           
            return  View("~/Views/Home/Page404.cshtml");
              
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

     

     
    }
}
