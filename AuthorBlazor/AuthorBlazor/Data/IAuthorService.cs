using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorBlazor.Models;

namespace AuthorAPI.Data
{
    public interface IAuthorService
    {
        Task<Author> GetAsync(int id);
        Task AddAuthorAsync(Author author);
        Task<IList<Author>> GetAuthorsAsync();

        Task<string> GetAuthorFirstName(int id);

        Task<string> GetAuthorLastName(int id);
    }
}