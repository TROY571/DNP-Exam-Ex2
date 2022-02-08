using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.Persistence
{
    public class AuthorDBContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Author.db");
        }
    }
}