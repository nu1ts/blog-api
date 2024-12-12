using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class CreatePostDto
{
    [MinLength(5)]
    public string Title { get; set; }

    [Required]
    [MinLength(5)]
    public string Description { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "ReadingTime must be positive")]
    public int ReadingTime { get; set; }
    
    [Url]
    public string Image { get; set; }
    
    //[ValidAddressId]
    public Guid? AddressId { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "There must be at least one tag")]
    public List<Guid> Tags { get; set; }
}