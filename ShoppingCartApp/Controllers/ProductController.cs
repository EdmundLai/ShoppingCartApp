using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCartApp.Data;
using ShoppingCartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Controllers
{
    [Authorize(Roles="Admin")]
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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _shoppingAppContext.Products.Add(product);
                    await _shoppingAppContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction("Index");
            }

            var categoryList = _shoppingAppContext.Categories.ToList();

            ViewData["categoryList"] = categoryList;

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _shoppingAppContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            var categoryList = _shoppingAppContext.Categories.ToList();

            ViewData["categoryList"] = categoryList;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var oldProduct = await _shoppingAppContext.Products.FindAsync(id);

                    oldProduct.CategoryId = product.CategoryId;
                    oldProduct.ProductName = product.ProductName;
                    oldProduct.ProductCost = product.ProductCost;

                    _shoppingAppContext.Update(oldProduct);
                    await _shoppingAppContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }

                return RedirectToAction("Index");
            }

            var categoryList = _shoppingAppContext.Categories.ToList();

            ViewData["categoryList"] = categoryList;

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _shoppingAppContext.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id, bool notUsed)
        {
            var currProduct = await _shoppingAppContext.Products.FindAsync(id);
            _shoppingAppContext.Remove(currProduct);
            await _shoppingAppContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
