using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppLibrary.Models;
using WebAppLibrary.Repositories.Interface;

namespace WebAppLibrary.Repositories
{
    public class LibraryRepository: ILibraryRepository
    {
        private LibraryContext _context;
        private ILogger<LibraryRepository> _logger;

        public LibraryRepository(LibraryContext context, ILogger<LibraryRepository> logger) {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Book> GetAllBooks() {
            try {
                _logger.LogInformation("All Books will be returned");
                return _context.books.OrderBy(b => b.Name).ToList();
            }
            catch (Exception e) {
                _logger.LogError("Could not get books from DB", e);
                return null;
            }            
        }

        public IEnumerable<Book> GetBooksByName()
        {
            try
            {
                _logger.LogInformation("All Books by Name will be returned");
                return _context.books.Include(b => b.Name).OrderBy(b => b.Name).ToList();
            }
            catch (Exception e) {
                return null;
            }
        }

        public ShoppingCart GetSalesById(int salesId)
        {
            try
            {
                _logger.LogInformation("Sales by Id will be returned");
                return _context.ordersClient.Include(b => b.bookOrders).Where(b => b.Id == salesId).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<Book> GetBooksByAuthor()
        {
            return _context.books.Include(b => b.Author).OrderBy(b => b.Name).ToList();
        }

        public void AddBook(Book newBook)
        {
            _context.Add(newBook);
        }

        public void AddShoppingCart(ShoppingCart newShoppingCart) {
            _context.Add(newShoppingCart);
        }

        public bool SaveAll()
        {
            //Returns an integer 
            return _context.SaveChanges() > 0;      //If changes are > 0, are saved
        }
    }
}
