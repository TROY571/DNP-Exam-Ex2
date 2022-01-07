using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthorAPI.Models;
using AuthorAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthorAPI.Data
{
    public class BookSqliteService:IBookService
    {
        AuthorDBContext authorDbContext;

        public BookSqliteService()
        {
            authorDbContext = new AuthorDBContext();
        }
        
        public async Task<IList<Book>> GetBooksAsync()
        {
            return await authorDbContext.Books.ToListAsync();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var list = authorDbContext.Books;
            foreach (var check in list)
            {
                if (check.ISBN == book.ISBN)
                {
                    Console.WriteLine("Book alreadly existed.");   
                }
                else
                {
                    EntityEntry<Book> bookAdd = await authorDbContext.Books.AddAsync(book);
                    await authorDbContext.SaveChangesAsync();
                    return bookAdd.Entity;
                }
            }
            return null;
        }

        public async Task RemoveBookAsync(int bookId)
        {
            Book bookremove = await authorDbContext.Books.FirstAsync(book => book.ISBN == bookId);
            if (bookremove != null)
            {
                authorDbContext.Books.Remove(bookremove);
                await authorDbContext.SaveChangesAsync();
            }
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            try
            {
                Book toUpdate = await authorDbContext.Books.FirstOrDefaultAsync(b => b.ISBN == book.ISBN);
                toUpdate.ISBN = book.ISBN;
                toUpdate.Title = book.Title;
                toUpdate.PublicationYear = book.PublicationYear;
                toUpdate.NumOfPages = book.NumOfPages;
                toUpdate.Genre = book.Genre;
                toUpdate.AuthorId = book.AuthorId;
                authorDbContext.Update(toUpdate);
                await authorDbContext.SaveChangesAsync();
                return toUpdate;
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find book with ISBN{book.ISBN}");
            }
            
        }

        public async Task<Book> SearchBook(int bookId)
        {
            var list = authorDbContext.Books;
            foreach (var book in list)
            {
                if (book.ISBN == bookId)
                {
                    return book;
                }
            }
            return null;
        }
    }
}