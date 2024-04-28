using Microsoft.AspNetCore.Mvc;
using Data;
using System.Text.Json;
using Core.Services;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet(Name = "GetUserOrder")]
    public IActionResult GetUserOrders(int userId)
    {
        if (!(userId > 0))
            return BadRequest();


        return Ok();
    }

    [Route("place-order")]
    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        if (order == null || !(order.Products?.Count > 0))
            return BadRequest();

        try
        {
            bool succes = _orderService.PlaceOrder(order);

            return succes ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
