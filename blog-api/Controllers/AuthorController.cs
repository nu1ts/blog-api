using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers;

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("list")]
    [ProducesResponseType(typeof(List<AuthorDto>), 200)]
    [ProducesResponseType(typeof(Response), 500)]
    public IActionResult GetAuthors()
    {
        var authors = _authorService.GetAuthors();
        return Ok(authors);
    }
}