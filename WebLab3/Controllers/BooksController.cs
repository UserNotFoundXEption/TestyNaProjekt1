using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using WebLab3;
using static System.Runtime.InteropServices.JavaScript.JSType;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> Books = new List<Book>();



    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
        if (Library.AddBook(book))
        {
            return Ok(new { message = "Book added" });
        }
        else
        {
            return BadRequest(new { message = "Bad book info" });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        if (Library.DeleteBook(id))
        {
            return Ok(new { message = "Book deleted" });
        }
        return NotFound(new { message = "Book not found" });
    }

    [HttpGet]
    public IActionResult GetBooks([FromQuery] string type)
    {
        if (type == "list")
        {
            return Ok(Library.GetBooks());
        }
        if (type == "chart")
        {
            return Ok(Library.GetChartValues());
        }
        return BadRequest(new { message = "Bad target" });
    }
}
