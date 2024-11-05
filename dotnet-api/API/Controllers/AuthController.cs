namespace API.Controllers;

using BCrypt.Net;
using DTOs;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly JwtHelper _jwtHelper;

    public AuthController(AppDbContext appDbContext, JwtHelper jwtHelper)
    {
        _appDbContext = appDbContext;
        _jwtHelper = jwtHelper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(x => x.Username == request.Username);
        if (user is null)
        {
            return NotFound(new { Error = "Tài khoản không tồn tại." });
        }

        if (!BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return NotFound(new { Error = "Tài khoản không tồn tại." });
        }

        var token = _jwtHelper.GenerateToken(user);

        return Ok(new { Token = token, UserId = user.Id });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var userExists = await _appDbContext.Users
            .AnyAsync(x => x.Username == request.Username);
        if (userExists)
        {
            return BadRequest(new { Error = "Tài khoản đã tồn tại." });
        }

        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.HashPassword(request.Password)
        };

        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        
        var token = _jwtHelper.GenerateToken(user);

        return Ok(new { Token = token, UserId = user.Id });
    }
}