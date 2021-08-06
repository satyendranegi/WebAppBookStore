using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBookStore.Models;

namespace WebAppBookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1,Title="MVC",Author = "Satyendra"},
                new BookModel(){Id=1,Title="Asp Net Core",Author = "Vikas"},
                new BookModel(){Id=1,Title="Java",Author = "Pankaj"},
                new BookModel(){Id=1,Title="PHP",Author = "Atul"},
                new BookModel(){Id=1,Title="Python",Author = "Ajit"},
            };
        }
    }
}
