using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TokenResponse),200)]
        [ProducesResponseType(typeof(Response),400)]
        [ProducesResponseType(typeof(Response),500)]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var tokenResponse = await _userService.RegisterUser(model);

            return Ok(tokenResponse);
        }
    }
}