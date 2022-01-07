using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorBlazor.Models;

namespace AuthorAPI.Data
{
    public interface IBookService
    {
        Task<IList<Book>> GetBooksAsync();
        
        Task AddBookAsync(Book book);

        Task RemoveBookAsync(int bookId);
        
        Task<Book> GetBookByIsbnAsync(int bookId);
        
        Task UpdateBookAsync(Book book);
    }
}