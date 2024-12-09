using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using TestApp1;
using static System.Runtime.InteropServices.JavaScript.JSType;

[ApiController]
[Route("api/[controller]")]
public class CountChangeController : ControllerBase
{
    private static List<Book> Books = new List<Book>();


    [HttpPost]
    public IActionResult Change([FromBody] string command)
    {
        bool increase = false;
        if (command[0] == 'i')
        {
            increase = true;
        }
        else if(command[0] != 'd')
        {
            return BadRequest(new { message = "Wrong command for increase/decrease" });
        }

        int id = int.Parse(command.Substring(1));

        if (increase && Library.IncreaseCount(id))
        {
            return Ok();
        }
        else if(Library.DecreaseCount(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest(new { message = "Couldn't decrease/increase" });
        }
    }
}
