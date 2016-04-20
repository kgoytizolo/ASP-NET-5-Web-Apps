using System;
using Microsoft.Data.Entity;        //Related to package EntityFramework.Core 7.0.0

namespace WebAppLibrary.Models
{
    public class LibraryContext : DbContext
    {
        //This library will contain Entity Framework DBContext concept. Represents the starting point to storage
        public DbSet<ShoppingCart> ordersClient { get; set; }
        public DbSet<Book> books { get; set; }
    }
}
