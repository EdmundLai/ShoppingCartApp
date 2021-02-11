using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(11,2)")]
        public decimal ProductCost { get; set; }

        [Range(1, 5000)]
        public int Quantity { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
