using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantManagerServer.Models;
using PlantManagerServer.Services;

namespace PlantManagerServer.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly PlantDbContext _dbContext;
    private readonly ILogger<AuthController> _logger;
    private readonly TokenService _tokenService;
    
    public AuthController(PlantDbContext dbContext, ILogger<AuthController> logger, TokenService tokenService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _dbContext.UserEntities.AddAsync(new UserTable()
        {
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password
        });
        
        var affectedRows = await _dbContext.SaveChangesAsync();
        if (affectedRows > 0)
        {
            return CreatedAtAction(nameof(Register), new {email = request.Email}, request);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await _dbContext.UserEntities.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            return Unauthorized();
        }

        if (!string.Equals(user.Password, request.Password, StringComparison.Ordinal))
        {
            return BadRequest("Bad credentials");
        }
        
        var token = _tokenService.CreateToken(user);
        return Ok(new AuthResponse()
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = token
        });
    }
    
}