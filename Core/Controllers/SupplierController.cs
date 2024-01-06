using Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    List<Supplier> suppliers = new()
    {
            new Supplier()
            {
                Id = 1,
                Name = "Test",
                Rating = 3.5,

            },
            new Supplier()
            {
                Id = 2,
                Name = "Test 2",
                Rating = 5
            },
            new Supplier()
            {
                Id = 3,
                Name = "Test 3",
                Rating = 4.5
            }
    };

    [HttpGet(Name = "GetSuppliers")]
    public IEnumerable<Supplier> GetSuppliers()
    {
        foreach (var supplier in suppliers)
        {
            string imagePath = $"C:\\Data\\Suppliers-Images\\{supplier.Id}-{supplier.Name}\\main.png";
            supplier.Image = ImageToByteArray(imagePath);
        }

        return suppliers;
    }

    private byte[] ImageToByteArray(string imagePath)
    {
        if (!System.IO.File.Exists(imagePath))
            return null;
        byte[] imgdata = System.IO.File.ReadAllBytes(imagePath);
        return imgdata;
    }

}
