using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.EF.Repositories
{
    public class EFBookRepository : IBookRepository
    {
        private readonly BookContext bookCtx;

      
        //costruttore quando é invocato associa un istanza do BookContext
        public EFBookRepository() 
        {
            bookCtx = new BookContext(); //read only field 
        }

        public bool Add(Book newBook)
        {
            if (newBook == null) return false;

            try
            {
                //bookCtx.Books.Add(newBook);
                //bookCtx.SaveChanges();

                var shelf = bookCtx.Shelves.FirstOrDefault(s => s.Id == newBook.Shelf.Id);
                shelf.Books.Add(newBook);

                bookCtx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }                  
        }

        public bool Delete(Book bookToDelete)
        {
            if(bookToDelete == null) return false;

            try
            {
                var book = bookCtx.Books.Find(bookToDelete.Id);

                if (book != null)
                    bookCtx.Books.Remove(bookToDelete);

                bookCtx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Book> Fetch()
        {          
           var books = bookCtx.Books.Include(b=> b.Shelf)
                .ToList();
            return books;            
        }

        public Book GetById(int id)
        {
            if (id <= 0)
                return null;

            return bookCtx.Books.Find(id);
        }

        public Book GetByIsbn(string isbn)
        {
            if(String.IsNullOrEmpty(isbn)) return null;

            try
            {
                var books = bookCtx.Books.Where(b => b.ISBN == isbn).FirstOrDefault();
                return books;
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public bool Update(Book updatedBook)
        {
            if (updatedBook == null) return false;
            try
            {
                bookCtx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
