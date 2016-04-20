using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public double UnitPrice { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
    }
}
