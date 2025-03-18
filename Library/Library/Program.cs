namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            library.AddBook(new Book("Ulysses", "James Joyce", "Macmillan"));
            library.AddBook(new Book("One Hundred Years of Solitude", "Gabriel Garcia Marquez", "Simon Schuster"));
            library.AddBook(new Book("Don Quixote", "Miguel de Cervantes", "HarperCollins"));
            library.AddBook(new Book("La Galatea", "Miguel de Cervantes", "HarperCollins"));

            library.BorrowBook("UlYsSeS");
            library.BorrowBook("UlYsSeS");

            List<Book> booksByAuthor = library.SearchByAuthor("miguel DE CERVANtes");
            Console.WriteLine("Miguel de Cervantes's books: ");
            foreach (var book in booksByAuthor)
            {
                Console.WriteLine($"- {book.Title}, {book.Publisher}");
            }

            List<Book> booksByTitle = library.SearchByTitle("one hundred years of solitude");
            Console.WriteLine("\nBooks Titled One Hundred Years of solitude ':");
            foreach (var book in booksByTitle)
            {
                Console.WriteLine($"- {book.Title} (by: {book.Author})");
            }

            library.ChangePublisher("Dupa", booksByAuthor.First());
            
            List<Book> booksByPublisher = library.SearchByPublisher("Dupa");
            Console.WriteLine("\nDUPA PUBLISHES':");
            foreach (var book in booksByPublisher)
            {
                Console.WriteLine($"- {book.Title} (by: {book.Author})");
            }
        }
    }
}