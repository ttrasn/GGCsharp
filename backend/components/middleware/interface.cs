using backend.services;

namespace backend.components.middleware;

public interface IAuthenticationService
{
    AuthenticateResponse Authenticate(UserService service, AuthenticateRequest model);
}