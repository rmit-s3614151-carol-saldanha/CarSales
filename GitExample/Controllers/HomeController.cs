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
        public async Task<IActionResult> Git(string userName)
        {
            return View( Program.repositories);
        }

        public async Task<IActionResult> Search(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                Program.getUserName(userName);
                ViewBag.userName = userName;
                return View(await Program.search());
            }
            else
            {

                return View("~/Views/Home/Page404.cshtml");

            }
              
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
