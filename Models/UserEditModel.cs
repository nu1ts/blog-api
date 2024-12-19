using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class UserEditModel
{
    [Required]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
}