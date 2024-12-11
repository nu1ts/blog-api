namespace blog_api.Exceptions;

public class PhoneException : Exception
{
    public PhoneException(string phone) 
        : base($"Phone '{phone}' is already taken.") { }
}