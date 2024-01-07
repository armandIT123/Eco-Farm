using Microsoft.AspNetCore.Mvc;
using Data;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public OrderController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet(Name = "GetUserOrder")]
    public IActionResult GetUserOrders(int userId)
    {
        return Ok();
    }

    [HttpPost(Name = "MakeOrder")]
    public IActionResult MakeOrder()
    {
        return BadRequest();
    }
}
