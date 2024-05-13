using backend.models;
using backend.services;
using Microsoft.AspNetCore.Mvc;
using backend.components.middleware;

namespace backend.controllers;

[Route("auth")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthenticationService _authentication;
    private readonly string jwtSecret;

    public UserController(UserService service, AuthenticationService authService)
    {
        _userService = service;
        _authentication = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthenticateRequest request)
    {
        var response = this._authentication.Authenticate(this._userService, request);
        if (response == null)
        {
            return NotFound("Email or Password is wrong.");
        }

        return Ok(response);
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var user = _userService.FindByEmail(request.Email);
        if (user != null)
        {
            return Conflict("email already exists");
        }

        user = new User();
        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = HashPassword(request.Password);

        _userService.InsertUser(user);
        return Ok("User registered successfully");
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}

public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}