using blog_api.Data;
using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers;

[ApiController]
[Route("api")]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("comment/{id:guid}/tree")]
    [ProducesResponseType(typeof(List<CommentDto>), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> GetCommentTree(Guid id)
    {
        var commentTree = await _commentService.GetCommentTree(id);;
        return Ok(commentTree);
    }
    
    [HttpPost]
    [Authorize]
    [Produces("application/json")]
    [Route("post/{id:guid}/comment")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] CreateCommentDto comment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        await _commentService.AddComment(id, comment, userId);
        return Ok();
    }
    
    [HttpPut]
    [Authorize]
    [Produces("application/json")]
    [Route("comment/{id:guid}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentDto comment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        await _commentService.UpdateComment(id, comment, userId);

        return Ok();
    }
    
    [HttpDelete]
    [Authorize]
    [Produces("application/json")]
    [Route("comment/{id:guid}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(void), 401)]
    [ProducesResponseType(typeof(void), 403)]
    [ProducesResponseType(typeof(void), 404)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        
        await _commentService.DeleteComment(id, userId);

        return Ok();
    }
}