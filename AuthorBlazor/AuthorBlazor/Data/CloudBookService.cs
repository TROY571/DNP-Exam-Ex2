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
    public class CloudBookService : IBookService
    {
        private string uri = "https://localhost:5003";

        private readonly HttpClient client;

        public CloudBookService()
        {
            client = new HttpClient();
        }

        public async Task<IList<Book>> GetBooksAsync()
        {
            Task<string> stringAsync = client.GetStringAsync(uri + "/Book");
            string message = await stringAsync;
            Console.WriteLine(message);
            List<Book> result = JsonSerializer.Deserialize<List<Book>>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }

        public async Task AddBookAsync(Book book)
        {
            string bookAsJson = JsonSerializer.Serialize(book);
            HttpContent content = new StringContent(bookAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri + "/Book", content);
        }

        public async Task RemoveBookAsync(int bookId)
        {
            await client.DeleteAsync($"{uri}/Book/{bookId}");
        }

        public async Task<Book> GetBookByIsbnAsync(int bookId)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Book/{bookId}");
            string message = await stringAsync;
            Console.WriteLine(message);
            Book result = JsonSerializer.Deserialize<Book>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }

        public async Task UpdateBookAsync(Book book)
        {
            string bookAdJson = JsonSerializer.Serialize(book);
            HttpContent content = new StringContent(bookAdJson, Encoding.UTF8, "application/json");
            await client.PatchAsync($"{uri}/Book/{book.ISBN}", content);
        }
    }
}