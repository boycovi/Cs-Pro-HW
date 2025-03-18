using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();
        }
        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void BorrowBook(string title)
        {
            Book bookToBorrow = books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower() && !b.IsBorrowed);
            if (bookToBorrow != null)
            {
                bookToBorrow.IsBorrowed = true;
                Console.WriteLine($" '{bookToBorrow.Title}' is now borrowed");
            }
            else Console.WriteLine("Book is not found or already borrowed");
        }
        public List<Book> SearchByAuthor(string author) => books.Where(b=>b.Author.ToLower() == author.ToLower()).ToList();
        public List<Book> SearchByTitle(string title) => books.Where(b=>b.Title.ToLower() == title.ToLower()).ToList();
        public List<Book> SearchByPublisher(string publisher) => books.Where(b=>b.Publisher.ToLower() == publisher.ToLower()).ToList();

        public void ChangePublisher(string newPublisher, Book book)
        {
            book.Publisher = newPublisher;
        }

    }
}
