using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class TokenResponse
{
    [Required]
    [MinLength(1)]
    public string Token { get; set; }
}