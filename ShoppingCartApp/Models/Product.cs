using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal ProductCost { get; set; }
    }
}
