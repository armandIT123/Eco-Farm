using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using System.Runtime.Intrinsics.Arm;
using Data;

namespace Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IConfiguration _configuration;
    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Register(IdentityUser user)
    {
        if(user == null)
            return BadRequest();

        try
        {
            object[] values = { user.Email, user.PasswordHash, user.Name, user.Surname };
            bool success = DbManager.Insert(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users(post: true), values);
            return success ? Ok() : BadRequest();
        }
        catch(Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + "." + nameof(Register));
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
                    Name = values[2] as string,
                    Surname = values[3] as string,
                    
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
}
