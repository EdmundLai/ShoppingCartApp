using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Controllers
{
    [Authorize]
    public class CustomerCartController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ShoppingAppContext _shoppingAppContext;

        public CustomerCartController(UserManager<IdentityUser> userManager, ShoppingAppContext shoppingAppContext)
        {
            _userManager = userManager;
            _shoppingAppContext = shoppingAppContext;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);

            int cartId = _shoppingAppContext.ShoppingCarts.Where(cart => cart.UserId == userId)
                .Select(cart => cart.ShoppingCartId).FirstOrDefault();

            var userCartItems = _shoppingAppContext.CartItems.Where(cartItem => cartItem.ShoppingCartId == cartId);

            return View(userCartItems);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var currItem = await _shoppingAppContext.CartItems.FindAsync(id);

            _shoppingAppContext.CartItems.Remove(currItem);
            await _shoppingAppContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            int cartId = getCartId();

            var userCartItems = _shoppingAppContext.CartItems.Where(cartItem => cartItem.ShoppingCartId == cartId);

            _shoppingAppContext.CartItems.RemoveRange(userCartItems);
            await _shoppingAppContext.SaveChangesAsync();

            return View("CheckoutConfirmation");
        }

        public IActionResult CheckoutConfirmation()
        {
            return View();
        }


        private int getCartId()
        {
            string userId = _userManager.GetUserId(User);

            return _shoppingAppContext.ShoppingCarts.Where(cart => cart.UserId == userId)
                .Select(cart => cart.ShoppingCartId).FirstOrDefault();
        }
    }
}
