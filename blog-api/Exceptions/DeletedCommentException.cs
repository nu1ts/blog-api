namespace blog_api.Exceptions;

public class DeletedCommentException : Exception
{
    public DeletedCommentException(Guid commentId) 
        : base($"Comment with id: {commentId} was deleted") { }
}