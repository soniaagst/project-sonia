using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.DTOs.Requests;
using ParkingSystemAPI.Services.Auth;

namespace ParkingSystemAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private UserService _userService;

    public AuthController(TokenService tokenService, UserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        var existingUser = await _userService.GetUserByUsernameAsync(registerDto.Username);
        if (existingUser is not null) return Conflict("Username is already registered.");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        await _userService.AddUserAsync(username: registerDto.Username, password: hashedPassword);

        return Ok("User registered successfully.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var user = await _userService.GetUserByUsernameAsync(loginRequest.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
}
