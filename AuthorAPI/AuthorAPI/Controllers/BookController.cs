using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Threading.Tasks;
using AuthorAPI.Data;
using AuthorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Book>>> GetBooks()
        {
            try
            {
                IList<Book> books = await bookService.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            try
            {
                Book added = await bookService.AddBookAsync(book);
                return Created($"/{added.ISBN}",added);
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{ISBN:int}")]
        public async Task<ActionResult> DeleteBook([FromRoute] int ISBN)
        {
            try
            {
                await bookService.RemoveBookAsync(ISBN);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<Book>> UpdateBook([FromBody] Book book)
        {
            try
            {
                Book updated = await bookService.UpdateAsync(book);
                return Ok(updated);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult> GetBook(int bookId)
        {
            Task<Book> book = bookService.SearchBook(bookId);
            if (book != null)
            {
                return Ok(book);
            }
            Console.WriteLine(bookId);
            return BadRequest("book not found");
        }
    }
}