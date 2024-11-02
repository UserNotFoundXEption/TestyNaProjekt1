using System.Runtime.CompilerServices;

namespace WebLab3
{
    public class Library
    {
        private static List<Book> books = new List<Book>
        {
            new Book(0, "The Iliad", "Homer", 64, "Ancient"),
            new Book(1, "The Odyssey", "Homer", 92, "Ancient"),
            new Book(2, "The Divine Comedy", "Dante Alighieri", 47, "Middle Ages"),
            new Book(3, "The Canterbury Tales", "Geoffrey Chaucer", 85, "Middle Ages"),
            new Book(4, "Don Quixote", "Miguel de Cervantes", 77, "Renaissance"),
            new Book(5, "Paradise Lost", "John Milton", 36, "Renaissance"),
            new Book(6, "Hamlet", "William Shakespeare", 59, "Renaissance"),
            new Book(7, "Gulliver's Travels", "Jonathan Swift", 48, "Enlightenment"),
            new Book(8, "Candide", "Voltaire", 66, "Enlightenment"),
            new Book(9, "Émile, or On Education", "Jean-Jacques Rousseau", 54, "Enlightenment"),
            new Book(10, "Pride and Prejudice", "Jane Austen", 94, "Romanticism"),
            new Book(11, "Frankenstein", "Mary Shelley", 31, "Romanticism"),
            new Book(12, "Moby-Dick", "Herman Melville", 56, "Romanticism"),
            new Book(13, "Madame Bovary", "Gustave Flaubert", 69, "Victorian"),
            new Book(14, "War and Peace", "Leo Tolstoy", 73, "Victorian"),
            new Book(15, "Crime and Punishment", "Fyodor Dostoevsky", 27, "Victorian"),
            new Book(16, "The Picture of Dorian Gray", "Oscar Wilde", 43, "Victorian"),
            new Book(17, "The Great Gatsby", "F. Scott Fitzgerald", 84, "Modern"),
            new Book(18, "Ulysses", "James Joyce", 53, "Modern"),
            new Book(19, "One Hundred Years of Solitude", "Gabriel Garcia Marquez", 95, "Modern"),
            new Book(20, "The Road", "Cormac McCarthy", 61, "Modern"),
            new Book(21, "The Essays", "Michel de Montaigne", 33, "Baroque"),
            new Book(22, "The Lusiads", "Luís de Camões", 78, "Baroque")
        };
        private static string searchString;

        private Library() { }


        public static bool AddBook(Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.title) || string.IsNullOrEmpty(book.author) || book.copies <= 0)
            {
                return false;
            }

            // Ustaw unikalne ID
            book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;

            books.Add(book);
            return true;
        }

        public static List<Book> GetBooks()
        {
            return books;
        }

        public static List<Book> GetBooksBySearch()
        {
            if(searchString == null)
            {
                Console.WriteLine("seach string is null");
                return null;
            }

            string searchSmall = searchString.ToLower();
            List<Book> resultBooks = new List<Book>();
    
            foreach (Book book in books)
            {
                string title = book.title.ToLower();
                string author = book.author.ToLower();
                if (title.Contains(searchSmall) || author.Contains(searchSmall))
                {
                    resultBooks.Add(book);
                }
            }
            return resultBooks;
        }

        public static void SetSearchString(string searchString)
        {
            Library.searchString = searchString;
        }

        public static int[] GetChartValues()
        {
            int[] values = new int[8];
            string[] periods = new string[] { "Ancient", "Middle Ages", "Renaissance", "Baroque", "Enlightenment", "Romanticism", "Victorian", "Modern" };
            
            for(int i = 0; i < 8; i++)
            {
                int copies = 0;
                foreach(Book book in books)
                {
                    if(book.period == periods[i])
                    {
                        copies += book.copies;
                    }
                }
                values[i] = copies;
            }

            return values;
        }
        public static bool DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Nie znaleziono książki o ID: {id}");
                return false;
            }

            books.Remove(book);
            Console.WriteLine($"Książka o ID: {id} została usunięta");
            return true;
        }

        public static bool IncreaseCount(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Nie znaleziono książki o ID: {id}");
                return false;
            }

            book.copies++;
            return true;
        }

        public static bool DecreaseCount(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine($"Nie znaleziono książki o ID: {id}");
                return false;
            }

            book.copies--;
            return true;
        }

    }

    public class Book
    {
        public Book(int id, string title, string author, int copies, string period)
        {
            this.Id = id;
            this.title = title;
            this.author = author;
            this.copies = copies;
            this.period = period;
        }

        public int Id { get; set; } // Unikalny identyfikator książki
        public string title { get; set; }
        public string author { get; set; }
        public int copies { get; set; }
        public string period { get; set; }
    }

}
