using blog_api.Data;
using blog_api.Exceptions;
using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Controllers;

[Authorize]
[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;
    private readonly ApplicationDbContext _dbContext;

    public PostController(PostService postService, ApplicationDbContext dbContext)
    {
        _postService = postService;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    [AllowAnonymous]
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
        Guid? userId = null;

        if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out var parsedUserId))
        {
            userId = parsedUserId;
        }

        var postPagedList = await  _postService.GetPostList(tags, author, min, max,
            sorting, onlyMyCommunities, page, size, userId);
        return Ok(postPagedList);
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var authorIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(authorIdClaim) || !Guid.TryParse(authorIdClaim, out var authorId))
            return Unauthorized();
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == authorId) ?? throw new UserException();
        
        var postId = await _postService.CreatePost(createPostDto, user);
        
        return Ok(postId);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> GetPost(Guid id)
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        Guid? userId = null;

        if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out var parsedUserId))
        {
            userId = parsedUserId;
        }
        
        var postDto = await _postService.GetPost(id, userId);
        return Ok(postDto);
    }
    
    [HttpPost]
    [Produces("application/json")]
    [Route("{postId:guid}/like")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> AddLike(Guid postId)
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        await _postService.AddLike(postId, userId);
        return Ok();
    }
    
    [HttpDelete]
    [Produces("application/json")]
    [Route("{postId:guid}/like")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> RemoveLike(Guid postId)
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
    
        await _postService.RemoveLike(postId, userId);
        return Ok();
    }
}