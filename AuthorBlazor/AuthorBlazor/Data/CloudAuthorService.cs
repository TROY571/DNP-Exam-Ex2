using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthorAPI.Data;
using AuthorBlazor.Models;

namespace AuthorBlazor.Data
{
    public class CloudAuthorService:IAuthorService
    {
        private string uri = "https://localhost:5003";

        private readonly HttpClient client;

        public CloudAuthorService()
        {
            client = new HttpClient();
        }
        
        public async Task AddAuthorAsync(Author author)
        {
            string authorAsJson = JsonSerializer.Serialize(author);
            HttpContent content = new StringContent(authorAsJson, Encoding.UTF8, "application/json");
            await client.PostAsync(uri+"/authors", content);
        }

        public async Task<IList<Author>> GetAuthorsAsync()
        {
            Task<string> stringAsync = client.GetStringAsync(uri+"/authors");
            string message = await stringAsync;
            Console.WriteLine(message);
            List<Author> result = JsonSerializer.Deserialize<List<Author>>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }
        
        public async Task<Author> GetAsync(int id)
        {
            var authorsAsync = await GetAuthorsAsync();
            Author authorById = authorsAsync.FirstOrDefault(a => a.Id.Equals(id));
            return authorById;
        }
        
        public async Task<String> GetAuthorFirstName(int id)
        {
            Author author = await GetAsync(id);
            return author.FirstName;
        }

        public async Task<String> GetAuthorLastName(int id)
        {
            Author author = await GetAsync(id);
            return author.LastName;
        }
    }
}