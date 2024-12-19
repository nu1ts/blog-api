namespace blog_api.Exceptions;

public class LoginException : Exception
{
    public LoginException() 
        : base("Login failed") { }
}