using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBookStore.Data;
using WebAppBookStore.Models;

namespace WebAppBookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                Title = model.Title,
                TotalPages = model.TotalPages
            };
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any()== true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Ctegory = book.Ctegory,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Ctegory = book.Ctegory,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }
            return null;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="MVC",Author = "Satyendra",Description = "This is the Description for MVC book",Ctegory="Programming",Language="English",TotalPages=134},
                new BookModel(){Id=2,Title="Asp Net Core",Author = "Vikas", Description="This is the Description for Asp.net Core book",Ctegory="Framework",Language="Japnese",TotalPages=147},
                new BookModel(){Id=3,Title="Java",Author = "Pankaj", Description="This is the Description for Java book",Ctegory="Developer",Language="Hindi",TotalPages=200},
                new BookModel(){Id=4,Title="PHP",Author = "Atul",Description="This is the Description for PHP book",Ctegory="Concept",Language="English",TotalPages=534},
                new BookModel(){Id=5,Title="Python",Author = "Ajit",Description="This is the Description for Python book",Ctegory="DevOps",Language="English",TotalPages=700},
            };
        }
    }
}
