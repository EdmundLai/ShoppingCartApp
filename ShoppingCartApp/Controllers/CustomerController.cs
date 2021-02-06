using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult SubmitNewUser()
        {
            //Console.WriteLine();

            return Content($"user: {Request.Form["name"]} and password: {Request.Form["password"]} ");
        }
    }
}
