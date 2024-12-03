using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class UserDto
{
    public required Guid Id { get; set; }
    
    public required DateTime CreateTime { get; set; }
    
    [MinLength(1)]
    public required string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public required Gender Gender { get; set; }
    
    [EmailAddress]
    [MinLength(1)]
    public required string Email { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
}