namespace WebLab3
{
    public class Library
    {
        private static Library instance;
        private List<Book> books = new List<Book>();

        public static Library GetInstance()
        {
            if (instance == null)
            {
                instance = new Library();
            }
            return instance;
        }

        public bool AddBook(Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.title) || string.IsNullOrEmpty(book.author) || book.copies <= 0)
            {
                return false;
            }
            books.Add(book);
            return true;
        }

        public List<Book> GetBooks()
        {
            return books;
        }
    }

    public class Book
    {
        public string title { get; set; }
        public string author { get; set; }
        public int copies { get; set; }
    }
}
