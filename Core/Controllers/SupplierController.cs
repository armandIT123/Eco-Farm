using Data;
using Microsoft.AspNetCore.Mvc;
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
                Image = "picture.png",
                Rating = 3.5,

            },
            new Supplier()
            {
                Id = 2,
                Name = "Test 2",
                Image = "picture.png",
                Rating = 5
            },
            new Supplier()
            {
                Id = 3,
                Name = "Test 3",
                Image = "picture.png",
                Rating = 4.5
            }
    };

    //public IActionResult Index()
    //{
    //    return View();
    //}

    [HttpGet(Name = "GetSuppliers")]
    public IEnumerable<Supplier> GetSuppliers()
    {
        return suppliers;
    }
}
