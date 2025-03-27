using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.DTOs.Requests;
using ParkingSystemAPI.Services.Auth;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private UserApiService _userApiService;

    public AuthController(TokenService tokenService, UserApiService userApiService)
    {
        _tokenService = tokenService;
        _userApiService = userApiService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        var existingUser = await _userApiService.GetUserByUsername(registerDto.Username);
        if (existingUser is not null) return Conflict("Username is already registered.");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        User newUser = await _userApiService.AddUser(username: registerDto.Username, password: hashedPassword);

        return Ok("User registered successfully.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var user = await _userApiService.GetUserByUsername(loginRequest.Username);
        if (user is null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
}
