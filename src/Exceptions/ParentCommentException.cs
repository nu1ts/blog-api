namespace blog_api.Exceptions;

public class ParentCommentException : Exception
{
    public ParentCommentException() 
        : base("Parent comment not found or does not belong to the post") { }
}