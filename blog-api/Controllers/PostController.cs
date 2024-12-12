using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PostPagedListDto), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> GetPostList(
        [FromQuery] List<Guid>? tags,
        [FromQuery] string? author,
        [FromQuery] int? min,
        [FromQuery] int? max,
        [FromQuery] PostSorting? sorting,
        [FromQuery] bool onlyMyCommunities = false,
        [FromQuery] int page = 1,
        [FromQuery] int size = 5
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var postPagedList = await  _postService.GetPostList(tags, author, min, max,
            sorting, onlyMyCommunities, page, size, userId);
        return Ok(postPagedList);
    }
}