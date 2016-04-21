using System;
using Microsoft.Data.Entity;        //Related to package EntityFramework.Core 7.0.0

namespace WebAppLibrary.Models
{
    public class LibraryContext : DbContext
    {
        //This library will contain Entity Framework DBContext concept. Represents the starting point to storage
        public LibraryContext() {
            Database.EnsureCreated();
        }

        public DbSet<ShoppingCart> ordersClient { get; set; }
        public DbSet<Book> books { get; set; }

        //We configure the type of connection (Oracle, Mongo, other relational / non relational BD, etc)
        //Configure DB Provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = Startup.Configuration["Data:LibraryContextCnx"];
            optionsBuilder.UseSqlServer(connectionString);  //Expects connection string
            //Use of Entity Framework for DB migration - versions and schema (build as code or tooling)
            //Use Entity Framework commands for Migration using cmd command line (run cmd at project's level) > dnx ef
            //Create your migration with the following command: > dnx ef migrations add InitialDatabase
            //Then, check that migrations folder has been created with an initial Database Schema and corresponding Model Snapshot
            base.OnConfiguring(optionsBuilder);
        }

    }
}
