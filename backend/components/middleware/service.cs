using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.models;
using backend.services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.components.middleware;

public class AuthenticationService : IAuthenticationService
{
    private readonly byte[] jwt_secret;
    private readonly string jwt_issuer;

    public AuthenticationService(IOptions<AppSettings> appSettings)
    {
        this.jwt_secret = Encoding.UTF8.GetBytes(appSettings.Value.JwtSecret.PadRight(64, '-'));
        this.jwt_issuer = appSettings.Value.JwtIssuer;
    }

    public AuthenticateResponse Authenticate(UserService service, AuthenticateRequest request)
    {
        User? user = service.FindByEmail(request.Email);
        if (user == null) return null;
        if (!this.VerifyPassword(request.Password, user.Password)) return null;
        var token = generateToken(user);
        return new AuthenticateResponse() { Token = token };
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    private string generateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(jwt_secret);
        var credetial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Id", Convert.ToString(user.Id)),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("Role", Convert.ToString(user.UserType)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var token = new JwtSecurityToken(jwt_issuer, jwt_issuer, claims,
            expires: DateTime.UtcNow.AddDays(7), signingCredentials: credetial);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
