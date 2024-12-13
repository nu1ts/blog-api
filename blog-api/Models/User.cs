using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class User
{
    public Guid Id { get; set; }
    
    public DateTime CreateTime { get; set; }
    
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string FullName { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public List<Guid>? Posts { get; set; }
    
    public List<Guid>? Likes { get; set; }
}