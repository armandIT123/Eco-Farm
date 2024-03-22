using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Http;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UserController(IConfiguration configuration, IUserService userService, IJwtService jwtService)
    {
        _configuration = configuration;
        _userService = userService;
        _jwtService = jwtService;
    }

    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromBody] RegisterDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Invalid body");

        int? id = _userService.Register(user);

        if (!(id > 0))
            return BadRequest("Could not register user");

        string token = _jwtService.GenerateJwtToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], (int)id, user.Name);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(90),
            SameSite = SameSiteMode.None,
            Secure = true // Use this option only if your site uses HTTPS
        };
        Response.Cookies.Append("authToken", token, cookieOptions);

        return Ok(id);
    }

    [Route("check-auth-status")]
    [HttpGet]
    public IActionResult CheckAuthStatus()
    {
        var token = Request.Cookies["authToken"];
        // token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxNyIsInVuaXF1ZV9uYW1lIjoic2RmZyIsIm5iZiI6MTcxMDM2OTk4MywiZXhwIjoxNzE4MTQ1OTgzLCJpYXQiOjE3MTAzNjk5ODN9.RZHLT-jmmgvLN-ro12Cdgv6o0LdE9ceONrpTWeMb6cg";
        if (string.IsNullOrEmpty(token) || !_jwtService.ValidateToken(_configuration["Jwt:Key"] ?? "", token, out var principal))
            return Unauthorized();

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userId, out var id) && _userService.GetUser(id, out IdentityUser? user))
        {
            return Ok(user);
        }
        return NotFound();
    }

    [Route("logout")]
    [HttpPost]
    public IActionResult LogOut()
    {
        var deleteCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(-1),
            SameSite = SameSiteMode.Lax,
            //Secure = true // Use this option only if your site uses HTTPS
        };
        Response.Cookies.Append("authToken", "", deleteCookieOptions);
        return Ok();
    }

    [Route("CheckEmail")]
    [HttpPost]
    public IActionResult CheckEmail([FromBody] RegisterDTO registerDto)
    {
        // return Ok(email);
        if (string.IsNullOrEmpty(registerDto.Email))
            return BadRequest(false);

        bool? existingUser = DbManager.EmailExists(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Users), registerDto.Email);
        return existingUser != null ? Ok(existingUser) : BadRequest(false);
    }

    [Route("login")]
    [HttpPost]
    public IActionResult Login([FromBody] RegisterDTO login)
    {
        if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            return BadRequest(null);

        var user = _userService.Login(login.Email, login.Password);
        if (user == null || user.Id == 0)
            return NotFound("Email or passwords are incorect");
        else
        {
            string token = _jwtService.GenerateJwtToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], user.Id, user.UserName);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(90),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("authToken", token, cookieOptions);

            return Ok(user);
        }
    }
}
