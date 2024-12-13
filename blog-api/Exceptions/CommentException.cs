namespace blog_api.Exceptions;

public class CommentException : Exception
{
    public CommentException(Guid commentId) 
        : base($"Post with id: {commentId} not found") { }
}