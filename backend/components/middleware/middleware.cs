using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.models;
using backend.services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.components.middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly byte[] jwt_secret;
    private readonly string jwt_issuer;

    public JwtMiddleware(RequestDelegate _next, IOptions<AppSettings> _appSettings)
    {
        this._next = _next;
        this.jwt_secret = Encoding.UTF8.GetBytes(_appSettings.Value.JwtSecret.PadRight(64, '-'));
        this.jwt_issuer = _appSettings.Value.JwtIssuer;
    }

    public async Task Invoke(HttpContext context, UserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            attachUserToContext(context, userService, token);
        }

        _next(context);
    }

    private void attachUserToContext(HttpContext context, UserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(jwt_secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwt_issuer,
                ValidAudience = jwt_issuer
            }, out SecurityToken validateToken);


            var jwtToken = (JwtSecurityToken)validateToken;
            var userId = jwtToken.Claims.FirstOrDefault(_ => _.Type == "Id").Value;
            context.Items["User"] = userService.GetById(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}