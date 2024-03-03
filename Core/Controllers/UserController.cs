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
        if (user == null)
            return BadRequest();

        CreatePasswordHash(user.Password, out string passwordHash, out string salt);
        IdentityUser identityUser = new IdentityUser();
        identityUser.PasswordHash = passwordHash;
        identityUser.SecurityStamp = salt;
        identityUser.Email = user.Email;
        identityUser.UserName = user.Name;

        try
        {
            object[] values = { identityUser.Email, identityUser.PasswordHash, identityUser.UserName };
            int? success = DbManager.InsertAndReturnId(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users, values);

            if (success > 0)
            {
                string jwt = GenerateToken(user.Email);
                return Ok(jwt);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + ". -->" + nameof(Register));
            return BadRequest(ex);
        }
    }

    [Route("CheckEmail")]
    [HttpPost]
    public IActionResult CheckEmail([FromBody] string email)
    {
        return Ok(email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(false);

        bool? existingUser = DbManager.EmailExists(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Users), email);
        return existingUser != null ? Ok(existingUser) : BadRequest(false);
    }

    [HttpGet]
    public IActionResult Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return BadRequest(null);

        try
        {
            string sqlFilter = $"Email={email}";
            var values = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users, sqlFilter).FirstOrDefault();

            if (values?.Count > 0)
            {
                string storedHashedPassword = values[2] as string;
                string salt = values[3] as string;

                if (CheckPassword(password, storedHashedPassword, salt))
                {
                    IdentityUser currentlyLogedUser = new IdentityUser()
                    {
                        Id = Convert.ToInt32(values[0]),
                        Email = values[1] as string,
                    };

                    values = null;
                    storedHashedPassword = null;
                    salt = null;
                    return Ok(currentlyLogedUser);
                }
                else
                {
                    values = null;
                    storedHashedPassword = null;
                    salt = null;
                    return StatusCode(401);
                }
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + "." + nameof(Login));
            return BadRequest();
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
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = Encoding.UTF8.GetString(hmac.Key);
            var bytesPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordHash = Encoding.UTF8.GetString(bytesPasswordHash);
        }
    }

    private bool CheckPassword(string password, string hashedPassword, string passwordSalt)
    {
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(passwordSalt)))
        {
            var bytesPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            string passwordHash = Encoding.UTF8.GetString(bytesPasswordHash);
            return string.Equals(hashedPassword, passwordHash);
        }
    }
    #endregion
}
