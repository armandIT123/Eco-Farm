using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core;

internal class Tools
{
    internal static byte[] ImageToByteArray(string imagePath)
    {
        if (!System.IO.File.Exists(imagePath))
            return null;
        byte[] imgdata = System.IO.File.ReadAllBytes(imagePath);
        return imgdata;
    }



}
