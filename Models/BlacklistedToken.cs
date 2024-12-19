using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class BlacklistedToken
{
    public int Id { get; set; }

    [Required]
    [StringLength(2048)]
    public required string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}