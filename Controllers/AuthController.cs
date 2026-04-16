using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Medicos.Backend.Data.Models;
using Medicos.Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Medicos.Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    public AuthController(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "Paciente");

        return Ok("Usuario creado");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized("Credenciales inválidas");

        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
            return Unauthorized("Credenciales inválidas");

        var token = await GenerateJwt(user);

        return Ok(new { token });
    }

    private async Task<string> GenerateJwt(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user); 

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.UserName)
    };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}