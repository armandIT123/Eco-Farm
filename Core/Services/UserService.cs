using Core.Controllers;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services;

public interface IUserService
{
    public bool GetUser(int id, out IdentityUser? user);
    public int? Register(RegisterDTO user);
    public IdentityUser Login(string email, string password);
}

public class UserService : IUserService
{
    private IConfiguration _configuration;
    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IdentityUser? Login(string email, string password)
    {
        string sqlFilter = $"Email='{email}'";
        var values = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users, sqlFilter).FirstOrDefault();

        try
        {
            if (values?.Count > 0)
            {
                string storedHashedPassword = values[4] as string;
                string salt = values[5] as string;
                IdentityUser currentlyLogedUser = new IdentityUser();

                if (CheckPassword(password, storedHashedPassword, salt))
                {
                    int pos = 0;
                    currentlyLogedUser.Id = Convert.ToInt32(values[pos++]);
                    currentlyLogedUser.UserName = Convert.ToString(values[pos++]);
                    currentlyLogedUser.PhoneNumber = values[pos++] as string;
                    currentlyLogedUser.Email = values[pos++] as string;
                }
                values = null;
                storedHashedPassword = null;
                salt = null;

                return currentlyLogedUser;
            }
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public int? Register(RegisterDTO user)
    {
        CreatePasswordHash(user.Password, out string passwordHash, out string salt);
        IdentityUser identityUser = new IdentityUser();
        identityUser.PasswordHash = passwordHash;
        identityUser.SecurityStamp = salt;
        identityUser.Email = user.Email;
        identityUser.UserName = user.Name;

        try
        {
            object[] values = { identityUser.UserName, user?.PhoneNo ?? "", identityUser.Email, identityUser.PasswordHash, identityUser.SecurityStamp, DateTime.Now };
            int? id = DbManager.InsertAndReturnId(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users, values);

            return id;
        }
        catch (Exception ex)
        {
            Logger.Insert(ex.Message, LoggerMessageType.Error, ex.StackTrace ?? nameof(UserController) + ". -->" + nameof(Register));
            return null;
        }
    }

    public bool GetUser(int id, out IdentityUser? user)
    {
        user = null;
        string sqlFilter = $"Id={id}";
        var values = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Users), Tabels.Users, sqlFilter).FirstOrDefault();

        try
        {
            if (values?.Count > 0)
            {
                int pos = 1;
                user = new IdentityUser()
                {
                    Id = id,
                    UserName = values[pos++] as string,
                    PhoneNumber = values[pos++] as string,
                    Email = values[pos++] as string,
                };
                values = null;
            }
            return user != null;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            var bytesPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordHash = Convert.ToBase64String(bytesPasswordHash);
        }
    }

    private bool CheckPassword(string password, string hashedPassword, string passwordSalt)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt)))
        {
            var bytesPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            string passwordHash = Convert.ToBase64String(bytesPasswordHash);
            return string.Equals(hashedPassword, passwordHash);
        }
    }

}
