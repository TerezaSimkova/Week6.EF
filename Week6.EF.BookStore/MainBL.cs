using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore
{
    class MainBL
    {
        private IBookRepository _bookRepo;
        private IShelfRepository _shelfRepo;


        public MainBL(IBookRepository bookRepository, IShelfRepository shelfRepository)
        {
            _bookRepo = bookRepository;
            _shelfRepo = shelfRepository;
        }
        public List<Book> FetchBooks()
        {
            var books = _bookRepo.Fetch();
            return books;
        }

        internal Book GetByIsbn(string isbn)
        {
            if (String.IsNullOrEmpty(isbn)) throw new ArgumentNullException();

            var book = _bookRepo.GetByIsbn(isbn);
            return book;
        }

        internal void AddBook(Book newBook)
        {
            if(newBook == null) throw new ArgumentNullException();

            _bookRepo.Add(newBook);
        }

        internal void DeleteBook(Book bookToDelete)
        {
            if (bookToDelete == null) throw new ArgumentNullException();

            _bookRepo.Delete(bookToDelete);
        }

        internal void UpdateBook(Book bookToUpdate)
        {
            if (bookToUpdate == null) throw new ArgumentNullException();

            _bookRepo.Update(bookToUpdate);
        }

        internal List<Shelf> FetchScehlves()
        {
            return _shelfRepo.Fetch();
        }

        internal Shelf GetByCode(string code)
        {
            //validazione
            var shelf = _shelfRepo.GetByCode(code);
            return shelf;
        }
    }

    //piu o meno bussines layer - userá repository di EF per accedere ai dati ma senza sapere a quale repository, accederá ad una interfaccia
    //fa tramitte ,ce un mottore che genere tutto per noi
}
