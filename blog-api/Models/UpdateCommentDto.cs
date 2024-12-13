using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class UpdateCommentDto
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; }
}