using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.models;
using Microsoft.IdentityModel.Tokens;

namespace backend.components.jwt;

public class JwtSecurityTokenHandlerWrapper
{
    private readonly string jwtSecret;
    private readonly string issuer;
    private readonly string audience;

    public readonly TokenValidationParameters PrincipleConfig;

    public JwtSecurityTokenHandlerWrapper()
    {
        jwtSecret = "asdasd".PadRight(64, 'a');
        audience = "Customer";
        issuer = "Games Global";
        PrincipleConfig = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    }

    // Generate a JWT token based on user ID and role
    public string GenerateJwtToken(User user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    // Validate a JWT token
    public ClaimsPrincipal ValidateJwtToken(string token)
    {
        // Retrieve the JWT secret from environment variables and encode it
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!);

        try
        {
            // Create a token handler and validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, PrincipleConfig, out SecurityToken validatedToken);

            return claimsPrincipal;
        }
        catch (Exception)
        {
            throw new ApplicationException("Token has expired.");
        }
    }
}