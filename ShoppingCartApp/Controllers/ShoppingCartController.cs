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
    public class ShoppingCartController : Controller
    {
        ShoppingAppContext _shoppingAppContext;

        public ShoppingCartController(UserManager<IdentityUser> userManager, ShoppingAppContext shoppingAppContext)
        {
            _shoppingAppContext = shoppingAppContext;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var shoppingCarts = _shoppingAppContext.ShoppingCarts.ToList();

            return View(shoppingCarts);
        }
    }
}
