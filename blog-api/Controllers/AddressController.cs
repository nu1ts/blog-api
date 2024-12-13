using blog_api.Models;
using blog_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }
    
    [HttpGet]
    [Route("search")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SearchAddressModel), 200)]
    [ProducesResponseType(typeof(Response), 500)]
    public async Task<IActionResult> Search(long parentObjectId, string? query)
    {
        var list = await _addressService.Search(parentObjectId, query);
        return Ok(list);
    }
}