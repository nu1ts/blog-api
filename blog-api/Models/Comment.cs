﻿using System.ComponentModel.DataAnnotations;

namespace blog_api.Models;

public class Comment
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Content { get; set; }
    
    public DateTime? ModifiedDate { get; set; }
    
    public DateTime? DeleteDate { get; set; }
    
    [Required]
    public Guid AuthorId { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Author { get; set; }
    
    [Required]
    public int SubComments { get; set; }
    
    public List<Comment> SubCommentsList { get; set; }
    
    public Guid? ParentId { get; set; }
    
    public Guid PostId { get; set; }
}