using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorAPI.Models;

namespace AuthorAPI.Data
{
    public interface IAuthorService
    {
        Task<Author> AddAuthorAsync(Author author);
        Task<IList<Author>> GetAuthorsAsync();
    }
}