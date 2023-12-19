using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

public class SupplierController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetSuppliers()
    {
        return Ok();
    }
}
