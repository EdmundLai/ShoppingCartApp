using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
