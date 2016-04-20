using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrary.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public double total { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public ICollection<Book> bookOrders { get; set; }
    }
}
