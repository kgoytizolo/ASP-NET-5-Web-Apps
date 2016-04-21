using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrary.Models
{
    public class LibraryContextSeedData
    {
        private LibraryContext _context;

        public LibraryContextSeedData(LibraryContext context) {
            _context = context;
        }

        public void EnsureSeedData() {
            //Seed information in the database
            if (!_context.books.Any()) {
                //Add new data
                var libraryBook1 = new Book()
                {                 
                    Name = "The Little Prince",
                    Created = DateTime.UtcNow,
                    Author = "Antoine de Saint-Exupéry",
                    UnitPrice = 6.70,
                    UserName = "kgoytizolo",
                    Location = "Latitude = 33.748995, Longitude = -84.387982"
                    //Id = 1,
                };

                _context.books.Add(libraryBook1);

                var libraryBook2 = new Book()
                {
                    Name = "Brave New World",
                    Created = DateTime.UtcNow,
                    Author = "Aldoux Huxley",
                    UnitPrice = 8.81,
                    UserName = "kgoytizolo",
                    Location = "Latitude = 33.748995, Longitude = -84.387982"
                    //Id = 2,
                };

                _context.books.Add(libraryBook2);
                _context.SaveChanges();
            }
        }
    }
}
