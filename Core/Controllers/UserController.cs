using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;
    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Register(RegisterDTO user)
    {
        if(user == null)
            return BadRequest();

        if (DbManager.EmailExists(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), user.Email))
            return BadRequest("Acest email este deja folosit");

        CreatePasswordHash(user.Password, out string passwordHash, out string salt);
        IdentityUser identityUser = new IdentityUser();
        identityUser.PasswordHash = passwordHash;
        identityUser.SecurityStamp = salt;
        identityUser.Email = user.Email;
        identityUser.UserName = user.Name;

        try
        {
            object[] values = { identityUser.Email, identityUser.PasswordHash,  identityUser.UserName};
            bool success = DbManager.Insert(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users(post: true), values);

            if (success)
            {
                string jwt = GenerateToken(user.Email);
                return Ok(jwt);
            }

            return BadRequest();
        }
        catch(Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + ". -->" + nameof(Register));
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult Login(string email, string password)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return BadRequest();

        try
        {
            string sqlFilter = $"Email={email} and Password={password}";
            var values = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users(get: true), sqlFilter).FirstOrDefault();

            if(values?.Count > 0)
            {
                IdentityUser currentlyLogedUser = new IdentityUser()
                {
                    Id = Convert.ToInt32(values[0]),
                    Email = values[1] as string,
                    
                };
                values = null;
            }

            return BadRequest();
        }
        catch(Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + "." + nameof(Login));
            return BadRequest(ex);
        }
    }

    #region Methods
    private string GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        var claims = new List<Claim>() { new Claim(ClaimTypes.Email, email) };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddDays(100),
            signingCredentials: credentials,
            claims: claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using(var hmac = new HMACSHA512())
        {
            passwordSalt = Encoding.UTF8.GetString(hmac.Key);
            var bytesPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordHash = Encoding.UTF8.GetString(bytesPasswordHash);            
        }
    }
    #endregion
}
