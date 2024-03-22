using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public interface IJwtService
{
    public bool ValidateToken(string key, string token, out ClaimsPrincipal principal);
    public string GenerateJwtToken(string key, string iss, string audience, int id, string name);
}

public class JwtService : IJwtService
{
    public bool ValidateToken(string? key, string token, out ClaimsPrincipal principal)
    {
        principal = null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters(key);

        try
        {
            principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return validatedToken != null;
        }
        catch
        {
            // Token validation failed
            return false;
        }
    }

    public string GenerateJwtToken(string key, string iss, string audience, int id, string name)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, name)
            // You can add more claims if needed
        }),
            Expires = DateTime.UtcNow.AddDays(90),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
            Issuer = iss,
            Audience = audience,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private TokenValidationParameters GetValidationParameters(string key)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero, // Optional: adjust the clock skew if necessary
            // You can also set ValidateLifetime = true if you want to check token expiration
        };
    }

}
