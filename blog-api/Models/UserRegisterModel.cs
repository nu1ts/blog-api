using System.ComponentModel.DataAnnotations;
using blog_api.Validation;

namespace blog_api.Models;

public class UserRegisterModel
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string FullName { get; set; }
    
    [Required]
    [MinLength(6)]
    [PasswordValidation]
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
}