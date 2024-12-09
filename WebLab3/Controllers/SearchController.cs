using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApp1;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    [HttpPost]
    public IActionResult SaveSearchInfo([FromBody] string searchString)
    {
        Library.SetSearchString(searchString);
        return Ok(new { message = "is good" });
    }

    [HttpGet]
    public IActionResult GetSearchResults()
    {
        return Ok(Library.GetBooksBySearch());
    }
}

