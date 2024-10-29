using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> Books = new List<Book>();

    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
        if (book == null || string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || book.Copies <= 0)
        {
            return BadRequest(new { message = "Bad book info" });
        }

        Books.Add(book);

        return Ok(new { message = "Book added" });
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(Books);
    }
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Copies { get; set; }
}
