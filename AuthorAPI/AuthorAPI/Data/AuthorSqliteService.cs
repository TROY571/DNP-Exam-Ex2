using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorAPI.Models;
using AuthorAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthorAPI.Data
{
    public class AuthorSqliteService:IAuthorService
    {
        AuthorDBContext authorDbContext;

        public AuthorSqliteService()
        {
            authorDbContext = new AuthorDBContext();
        }
        
        public async Task<Author> AddAuthorAsync(Author author)
        {
            var list = authorDbContext.Authors;
            foreach (var check in list)
            {
                if (check.FirstName == author.FirstName && check.LastName == author.LastName)
                {
                    Console.WriteLine("Author alreadly existed.");   
                }
                else
                {
                    EntityEntry<Author> authorAdd = await authorDbContext.Authors.AddAsync(author);
                    await authorDbContext.SaveChangesAsync();
                    return authorAdd.Entity;
                }
            }
            return null;
        }

        public async Task<IList<Author>> GetAuthorsAsync()
        {
            return await authorDbContext.Authors.Include(a => a.Books).ToListAsync();
        }
    }
}