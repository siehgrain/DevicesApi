using Devices.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var authSection = _configuration.GetSection("Auth");

        var configuredUsername = authSection["Username"];
        var configuredPassword = authSection["Password"];

        if (request.Username != configuredUsername ||
            request.Password != configuredPassword)
        {
            return Unauthorized();
        }

        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection["Key"]!;
        var expirationHours = int.Parse(jwtSection["ExpirationHours"]!);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username)
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expirationHours),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Ok(new LoginResponseDto
        {
            Token = tokenString
        });
    }
}
