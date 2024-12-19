namespace blog_api.Exceptions;

public class AddressException : Exception
{
    public AddressException() 
        : base("The provided AddressId does not exist.") { }
}