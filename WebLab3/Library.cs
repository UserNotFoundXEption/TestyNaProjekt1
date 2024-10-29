using System.Runtime.CompilerServices;

namespace WebLab3
{
    public class Library
    {
        private static List<Book> books = new List<Book>();
        private static string searchString;

        private Library() { }

        public static bool AddBook(Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.title) || string.IsNullOrEmpty(book.author) || book.copies <= 0)
            {
                return false;
            }
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
            string[] periods = new string[] { "Ancient", "Middle Ages", "Renaissance", "Baroque", "Enlightment", "Romanticism", "Victorian", "Modern" };
            
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
    }

    public class Book
    {
        public string title { get; set; }
        public string author { get; set; }
        public int copies { get; set; }
        public string period { get; set; }
    }
}
