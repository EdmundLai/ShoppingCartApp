using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Data;
using ShoppingCartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ShoppingAppContext _shoppingAppContext;

        public CustomerController(UserManager<IdentityUser> userManager, ShoppingAppContext shoppingAppContext)
        {
            _shoppingAppContext = shoppingAppContext;
            _userManager = userManager;
        }

        // returns true if new cart was created for the user
        public async Task<bool> CreateShoppingCartIfNotExists()
        {
            var userId = _userManager.GetUserId(User);

            var cart = _shoppingAppContext.ShoppingCarts.Where(cart => cart.UserId == userId).FirstOrDefault();

            // cart for user does not exist already
            if (cart == null)
            {
                ShoppingCart newCart = new ShoppingCart
                {
                    UserId = userId
                };

                await _shoppingAppContext.ShoppingCarts.AddAsync(newCart);
                await _shoppingAppContext.SaveChangesAsync();

                return true;
            }

            return false;
        }


        public async Task<IActionResult> Index()
        {
            await CreateShoppingCartIfNotExists();

            var uid = _userManager.GetUserId(User);

            ViewData["uid"] = uid;

            return View();
        }

    }
}
