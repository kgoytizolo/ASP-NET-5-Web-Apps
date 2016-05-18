using System.Collections.Generic;
using WebAppLibrary.Models;


namespace WebAppLibrary.Repositories.Interface
{
    public interface ILibraryRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetBooksByName();
        void AddBook(Book book);
        bool SaveAll();
        ShoppingCart GetSalesById(int bookId);
        void AddShoppingCart(ShoppingCart newShoppingCart);
    }
}
