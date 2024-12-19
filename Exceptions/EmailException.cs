namespace blog_api.Exceptions;

public class EmailException : Exception
{
    public EmailException(string email) 
        : base($"Username '{email}' is already taken.") { }
}