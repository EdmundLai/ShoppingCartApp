using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCartApp.Data;
using ShoppingCartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;

        ShoppingAppContext _shoppingAppContext;

        public ProductController(ILogger<ProductController> logger, ShoppingAppContext shoppingAppContext)
        {
            _logger = logger;
            _shoppingAppContext = shoppingAppContext;
        }

        public IActionResult Index()
        {
            var products = _shoppingAppContext.Products;

            return View(products);
        }

        public IActionResult Create()
        {
            var categoryList = _shoppingAppContext.Categories.ToList();

            ViewData["categoryList"] = categoryList;

            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _shoppingAppContext.Products.Add(product);
            _shoppingAppContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
