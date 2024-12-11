namespace blog_api.Exceptions;

public class UserException : Exception
{
    public UserException() 
        : base("User not found") { }
}