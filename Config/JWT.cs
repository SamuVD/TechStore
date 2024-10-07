using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechStoreFullRestAPI.Models;

namespace TechStoreFullRestAPI.Config;

public class JWT
{
    // Private method to generate the JWT.
    public static string GenerateJwtToken(User user)
    {
        // Create a security key using the secret key from the configuration.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT_KEY"));

        // Create signing credentials using the security key and HMAC-SHA256 algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Define the claims that will be included in the JWT (OPCIONAL).
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Name),
            new Claim("LastName", user.LastName),
            new Claim("NickName", user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()) // Aquí incluimos el rol
        };

        // Create the JWT with the configured parameters.
        var token = new JwtSecurityToken(
            issuer: "JWT_ISSUER", // Token issuer.
            audience: "JWT_AUDIENCE", // Token audience.
            claims: claims, // Claims to be included in the token.
            expires: DateTime.Now.AddMinutes(Convert.ToDouble("JWT_EXPIREMINUTES")), // Token expiration time.
            signingCredentials: credentials // Credentials for signing the token.
        );

        // Return the JWT as a string.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
