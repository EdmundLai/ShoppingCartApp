using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        UserManager<IdentityUser> _userManager;
        ShoppingAppContext _shoppingAppContext;

        public ShopController(UserManager<IdentityUser> userManager, ShoppingAppContext shoppingAppContext, ILogger<ShopController> logger)
        {
            _shoppingAppContext = shoppingAppContext;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _shoppingAppContext.Products.ToList();

            return View(products);
        }

        public async Task<IActionResult> ItemDetails(int id)
        {
            var product = await _shoppingAppContext.Products.FindAsync(id);

            int cartId = getCartId();

            _logger.LogInformation($"Shopping Cart ID: {cartId}");

            CartItem cartItem = new CartItem
            {
                ProductId = id,
                ProductName = product.ProductName,
                Quantity = 1,
                ShoppingCartId = cartId,
                ProductCost = product.ProductCost
            };

            return View(cartItem);
        }

        public async Task<IActionResult> AddToCart(CartItem newCartItem)
        {
            //_logger.LogInformation($"cart item id: {cartItem.CartItemId}");
            //_logger.LogInformation($"Product ID: {cartItem.ProductId}");
            //_logger.LogInformation($"Quantity: {cartItem.Quantity}");
            //_logger.LogInformation($"Product Name: {cartItem.ProductName}");
            //_logger.LogInformation($"Product Cost: {cartItem.ProductCost}");
            //_logger.LogInformation($"Product Cost: {cartItem.ShoppingCartId}");

            var existingItem = _shoppingAppContext.CartItems.Where(cartItem => cartItem.ProductId == newCartItem.ProductId).FirstOrDefault();

            if(existingItem != null)
            {
                existingItem.Quantity += newCartItem.Quantity;
                _shoppingAppContext.Update(existingItem);
            } else
            {
                await _shoppingAppContext.CartItems.AddAsync(newCartItem);
            }
            
            await _shoppingAppContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private int getCartId()
        {
            string userId = _userManager.GetUserId(User);

            return _shoppingAppContext.ShoppingCarts.Where(cart => cart.UserId == userId)
                .Select(cart => cart.ShoppingCartId).FirstOrDefault();
        }

    }
}
