using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class CreateCommentDto
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; }
    
    public Guid? ParentId { get; set; }
}