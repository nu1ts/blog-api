using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using blog_api.Options;
using Microsoft.IdentityModel.Tokens;

namespace blog_api.Services;

public class TokenService
{
    public string GenerateJwtToken(Guid userId)
    { 
        var issuedAt = DateTime.UtcNow;
        var expiresAt = issuedAt.AddMinutes(60);
        
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.NameId, userId.ToString()),
            new (JwtRegisteredClaimNames.Nbf, new DateTimeOffset(issuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new (JwtRegisteredClaimNames.Exp, new DateTimeOffset(expiresAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new (JwtRegisteredClaimNames.Iat, new DateTimeOffset(issuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new (JwtRegisteredClaimNames.Iss, AuthOptions.Issuer),
            new (JwtRegisteredClaimNames.Aud, AuthOptions.Audience)
        };
        
        var key = AuthOptions.GetSymmetricSecurityKey();
        var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credits
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}