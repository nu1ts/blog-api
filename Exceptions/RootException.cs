namespace blog_api.Exceptions;

public class RootException : Exception
{
    public RootException(Guid commentId) 
        : base($"Comment with {commentId} is not a root element") { }
}