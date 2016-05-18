using System;
using System.Collections.Generic;
using WebAppLibrary.Models;

namespace WebAppLibrary.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public double total { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public ICollection<Book> bookOrders { get; set; }
    }
}
