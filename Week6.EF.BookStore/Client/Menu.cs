using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Models;
using Week6.EF.BookStore.EF.Repositories;

namespace Week6.EF.BookStore.Client
{
    public class Menu
    {
        private static MainBL mainBL = new MainBL(new EFBookRepository(), new EFShelfRepository()); // con Mock sará EFMockRepository

        
        internal static void Start()
        {

            Console.WriteLine("Benvenuto!\n");

            char choice;

            do
            {
                Console.WriteLine("Premi 1 per aggiungere un libro");
                Console.WriteLine("Premi 2 per eliminare un libro");
                Console.WriteLine("Premi 3 per visualizzare tutti i libri in magazzino");
                Console.WriteLine("Premi 4 per modificare un libro");
                Console.WriteLine("Premi 5 per visualizzare i libri sul scaffale scelto dal utente.");
                Console.WriteLine("Premi Q per uscire\n");

                choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        //Aggiungi libro
                        AddNewBook();
                        Console.WriteLine();
                        break;
                    case '2':
                        //elimina libro
                        DeleteBook();
                        Console.WriteLine();
                        break;
                    case '3':
                        //visualizzare tutti i libri
                        ShowBooks();
                        Console.WriteLine();
                        break;
                    case '4':
                        //visualizzare libri per genere
                        UpdateQuantity();
                        Console.WriteLine();
                        break;
                    case '5':
                        ShowBooksOnShelf();
                        Console.WriteLine();
                        break;
                    case 'Q':
                        return;
                    default:
                        Console.WriteLine("Scelta non disponibile");
                        break;
                }
            }
            while (!(choice == 'Q'));


        }

        private static void ShowBooksOnShelf()
        {
            var books = mainBL.FetchBooks();
            Shelf shelf;
            do
            {
                Console.WriteLine("Inserisci il codice del scaffale di cui vedere i libri:\n");
                ShowShelves(); //mostra tutti i codici dei scaffali
                string code = Console.ReadLine();

                //reccupero lo scaffale con il codice, se esiste va avanti ,altrimenti m irichiede di inserire il codice
                shelf = GetShelfByCode(code);

                if (shelf == null)
                {
                    Console.WriteLine("Codice del scaffale innesistente!");
                }
                else
                {
                    foreach (var s in books)
                    {
                        if (s.Shelf.Code == shelf.Code) // devo accedere son S al libro poi al Shelf e poi al suo Code , per la sicurezza shelf.Code (per essere sicura del codice della mensola)
                        {
                            Console.WriteLine($"Libri sullo scaffale {s.Shelf.Code} sono : {s.Author} - {s.Title}");
                        }
                        
                    }
                }
               


            } while (shelf == null); //finché é diverso dal null chiede la domanda

           
        }



        private static void UpdateQuantity()
        {
            string isbn;

            Console.WriteLine("Digita il codice ISBN del libro di cui vuoi modificare la quantità");
            ShowBooks();

            do
            {
                Console.Write("\nInserisci il codice ISBN di 10 cifre:\n");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            var bookToUpdate = GetBookByISBN(isbn);

            if (bookToUpdate != null)
            {
                Console.WriteLine("Inserisci la quantitá");
                int quantity = 0;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Devi inserire la quantitá valida!");
                }
                bookToUpdate.Quantity = quantity;
                mainBL.UpdateBook(bookToUpdate);
            }
            else
            {
                Console.WriteLine("Non c'è un libro in magazzino con questo codice ISBN");
            }

        }

        private static void DeleteBook()
        {
            string isbn;
            Console.WriteLine("Digita il codice ISBN del libro che vuoi eliminare");
            ShowBooks();

            do
            {
                Console.Write("Inserisci il codice ISBN di 10 cifre:");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            var bookToDelete = GetBookByISBN(isbn);

            if (bookToDelete != null)
            {
                mainBL.DeleteBook(bookToDelete);
            }
            else
            {
                Console.WriteLine("Non c'è un libro in magazzino con questo codice ISBN");
            }
        }

        public static void AddNewBook()
        {
            //interazione con utente
            string title, author;
            string isbn;
            do
            {
                Console.Write("Inserisci il codice ISBN di 10 cifre:");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            if (GetBookByISBN(isbn) == null)
            {
                do
                {
                    Console.Write("Inserisci il titolo:");
                    title = Console.ReadLine();
                }
                while (title.Length == 0);

                do
                {
                    Console.Write("Inserisci l'autore:");
                    author = Console.ReadLine();

                } while (author.Length == 0);

                Console.Write("Inserisci la quantità che sarà disponibile in magazzino:");

                int quantity=0;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Devi inserire un valore valido");
                }

                //inserire scaffale
                Shelf shelf;
                do
                {
                    Console.WriteLine("Inserisci il codice del scaffale in cui posizionare i llibro:\n");
                    ShowShelves(); //mostra tutti i codici dei scaffali
                    string code = Console.ReadLine();

                    //reccupero lo scaffale con il codice, se esiste va avanti ,altrimenti m irichiede di inserire il codice
                    shelf = GetShelfByCode(code);

                } while (shelf == null); //finché é diverso dal null chiede la domanda

                Book newBook = new Book
                {
                    ISBN = isbn,
                    Title = title,
                    Author = author,
                    Quantity = quantity,
                    Shelf = shelf
                };

                mainBL.AddBook(newBook); //fare controllo se non inserisco niente if e try catch

                                                
               // Console.WriteLine($"Libro aggiunto. ISBN: {newBook.ISBN} - Titolo: {newBook.Title} - Autore: {newBook.Author} - Quantità: {newBook.Quantity} - Scaffale: {newBook.Shelf.Code}");
            }
            else
            {
                Console.WriteLine("Esiste già un libro con questo ISBN in magazzino");
            }

        }

        private static Shelf GetShelfByCode(string code)
        {
            var shelf = mainBL.GetByCode(code);
            return shelf;
        }

        private static void ShowShelves()
        {
            var shelves = mainBL.FetchScehlves();
            if (shelves.Count != 0)
            {
                Console.WriteLine($"Scaffali:");
                foreach (var s in shelves)
                {
                    Console.WriteLine(s.Code);
                }
            }
            else
            {
                Console.WriteLine("Non ci sono scaffali disponibili.");
            }
        }

        private static Book GetBookByISBN(string isbn)
        {
            var book = mainBL.GetByIsbn(isbn);
            return book;
        }

        public static void ShowBooks()
        {
            var books = mainBL.FetchBooks();

            Console.WriteLine("I libri in magazzino sono:");
            if (books.Count != 0)
            {
                foreach (var b in books)
                {
                    Console.WriteLine($"{b.ISBN}-{b.Author} - {b.Title} - {b.Quantity} Scaffale: {b.Shelf.Code}");
                }
            }
            else
            {
                Console.WriteLine("\nIn magazino non ci sono i libri!");
            }
        }


    }
}
