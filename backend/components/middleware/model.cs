using System.ComponentModel.DataAnnotations;

namespace backend.components.middleware;

public class AuthenticateResponse
{
    public string Token { get; set; }
}

public class AuthenticateRequest
{
    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
}
