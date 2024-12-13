namespace blog_api.Exceptions;

public class PostException : Exception
{
    public PostException(Guid postId) 
        : base($"Post with id: {postId} not found") { }

}