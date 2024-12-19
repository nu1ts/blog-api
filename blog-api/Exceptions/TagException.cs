namespace blog_api.Exceptions;

public class TagException : Exception
{
    public TagException() 
        : base("One or more of the provided Tags do not exist") { }
}