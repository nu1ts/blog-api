using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers;

[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly TagService _tagService;

    public TagController(TagService tagService)
    {
        _tagService = tagService;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TagDto), 200)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await _tagService.GetTags();
        return Ok(tags);
    }
}