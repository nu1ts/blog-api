using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace blog_api.Options;

public static class AuthOptions
{
    public const string Issuer = "lumpy-pies";
    public const string Audience = "blog";
    
    private const string Key = "VN7$g#nLkL59x!P@wYEc&Zu2sQXrbHT3oJ4p8A!mcD9^F*t6"; 

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        var keyBytes = Encoding.UTF8.GetBytes(Key);
        return new SymmetricSecurityKey(keyBytes);
    }
}