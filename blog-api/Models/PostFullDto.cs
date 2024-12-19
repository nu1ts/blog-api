using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class PostFullDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "ReadingTime must be positive")]
    public int ReadingTime { get; set; }
    
    [Url]
    public string? Image { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Author { get; set; }
    
    public Guid? CommunityId { get; set; }
    
    public string? CommunityName { get; set; }
    
    public Guid? AddressId { get; set; }

    [Required]
    public int Likes { get; set; }
    
    [Required]
    public bool HasLike { get; set; }

    [Required]
    public int CommentsCount { get; set; }
    
    public List<TagDto>? Tags { get; set; }
    
    [Required]
    public List<CommentDto> Comments { get; set; }
}