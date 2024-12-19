using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class TagDto
{
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
}