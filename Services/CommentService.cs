using blog_api.Data;
using blog_api.Exceptions;
using blog_api.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_api.Services;

public class CommentService
{
    private readonly ApplicationDbContext _dbContext;

    public CommentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CommentDto>> GetCommentTree(Guid commentId)
    {
        var rootComment = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);
        
        if (rootComment == null)
            throw new CommentException(commentId);
        
        if (rootComment.ParentId != null)
            throw new RootException(commentId);
        
        var commentTree = await CreateCommentTree(rootComment);
        return commentTree;
    }

    private async Task<List<CommentDto>> CreateCommentTree(Comment rootComment)
    {
        var subComments = await _dbContext.Comments
            .Where(c => c.ParentId == rootComment.Id)
            .ToListAsync();
    
        var commentDtos = new List<CommentDto>();
        
        foreach (var subComment in subComments)
        {
            var commentDto = new CommentDto
            {
                Id = subComment.Id,
                CreateTime = subComment.CreateTime,
                Content = subComment.Content,
                ModifiedDate = subComment.ModifiedDate,
                DeleteDate = subComment.DeleteDate,
                AuthorId = subComment.AuthorId,
                Author = subComment.Author,
                SubComments = 0
            };
            
            var childComments = await CreateCommentTree(subComment);
            commentDto.SubComments = childComments.Count;
            
            commentDtos.Add(commentDto);
            
            commentDtos.AddRange(childComments);
        }

        return commentDtos;
    }
    
    public async Task AddComment(Guid postId, CreateCommentDto createCommentDto, Guid userId)
    {
        var post = await _dbContext.Posts.FindAsync(postId);
        if (post == null)
            throw new PostException(postId);
        
        // TODO: Community validation
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new UserException();
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            CreateTime = DateTime.UtcNow,
            Content = createCommentDto.Content,
            AuthorId = userId,
            Author = user.FullName,
            SubComments = 0,
            PostId = postId,
            ParentId = createCommentDto.ParentId
        };
        
        _dbContext.Comments.Add(comment);
        
        if (createCommentDto.ParentId.HasValue)
        {
            var parentComment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == createCommentDto.ParentId.Value);
            
            if (parentComment == null || parentComment.PostId != postId)
            {
                throw new ParentCommentException();
            }
            parentComment.SubComments++;
        }
        else
        {
            post.Comments.Add(comment.Id);
        }
        
        post.CommentsCount++;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateComment(Guid commentId, UpdateCommentDto newContent, Guid userId)
    {
        var comment  = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment  == null)
            throw new CommentException(commentId);
    
        if (comment.AuthorId != userId)
            throw new UnauthorizedAccessException("You are not the author of this comment.");
        
        if (comment.DeleteDate != null)
            throw new DeletedCommentException(commentId);
        
        comment.Content = newContent.Content;
        comment.ModifiedDate = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteComment(Guid commentId, Guid userId)
    {
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
            throw new CommentException(commentId);

        // TODO: community valid
        if (comment.AuthorId != userId)
            throw new UnauthorizedAccessException("You are not the author of this comment.");

        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == comment.PostId);

        if (post == null)
            throw new PostException(comment.PostId);

        if (comment.DeleteDate != null)
        {
            if (comment.SubComments == 0)
            {
                _dbContext.Comments.Remove(comment);
                post.CommentsCount--;
            }
            else
            {
                throw new InvalidOperationException("Cannot delete a comment that is already marked as deleted but still has sub-comments.");
            }
        }
        else
        {
            if (comment.SubComments > 0)
            {
                comment.DeleteDate = DateTime.UtcNow;
                comment.Content = "";
            }
            else
            {
                _dbContext.Comments.Remove(comment);
                post.CommentsCount--;
            }
        }

        if (comment.ParentId != null)
        {
            var parentComment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == comment.ParentId);

            if (parentComment != null)
            {
                parentComment.SubComments--;
                
                if (parentComment.DeleteDate != null && parentComment.SubComments == 0)
                {
                    _dbContext.Comments.Remove(parentComment);
                    post.CommentsCount--;
                }
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}