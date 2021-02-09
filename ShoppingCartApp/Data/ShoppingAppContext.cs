using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Models;

namespace ShoppingCartApp.Data
{
    public class ShoppingAppContext : DbContext
    {
        public ShoppingAppContext(DbContextOptions<ShoppingAppContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
    }
}
