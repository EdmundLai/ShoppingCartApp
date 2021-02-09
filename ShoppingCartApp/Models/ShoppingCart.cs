using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public string UserId { get; set; }
    }
}
