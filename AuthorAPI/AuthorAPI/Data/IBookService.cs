using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorAPI.Models;

namespace AuthorAPI.Data
{
    public interface IBookService
    {
        Task<IList<Book>> GetBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task RemoveBookAsync(int bookId);
        Task<Book> UpdateAsync(Book book);
        
        Task<Book> SearchBook(int bookId);
    }
}