using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto loginRequest)
    {
        // Dummy check (replace with actual user authentication)
        if (loginRequest.Username == "admin" && loginRequest.Password == "password")
        {
            var token = _tokenService.GenerateToken(loginRequest.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized("Invalid credentials");
    }
}

public class LoginRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
