using Microsoft.AspNetCore.Mvc;
using Data;
using System.Text.Json;

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
        if (!(userId > 0))
            return BadRequest();

        string sqlFilter = $"UserId={userId}";
        string orderBy = "Date DESC";
        var orderRawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Orders), Tabels.Orders, sqlFilter, orderBy);

        if (orderRawData == null || orderRawData.Count == 0)
            return NotFound();

        List<Order> orders = new();
        foreach (var data in orderRawData)
        {
            int pos = 0;
            try
            {
                Order order = new()
                {
                    Id = (int)(decimal)data[pos++],
                    UserId = Convert.ToInt32(data[pos++]),
                    Message = data[pos++] as string,
                    PaymentMethod = (PaymentMethod)Convert.ToInt32(data[pos++]),
                    Status = (OrderStatus)Convert.ToInt32(data[pos++]),
                    Date = Convert.ToDateTime(data[pos++]),
                    Products = new()
                };
            }
            catch (Exception ex)
            {
               //logger
            }
        }
        return Ok();
    }

    [HttpPost(Name = "PlaceOrder")]
    public IActionResult PlaceOrder(Order order)
    {
        if(order == null || !(order.Products?.Count > 0))
            return BadRequest();

        object[] values = { order.UserId, JsonSerializer.Serialize(order.DeliveryAdress), order.Message, order.PaymentMethod, order.Status, order.Date };
        int? orderId = DbManager.InsertAndReturnId(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Orders), Tabels.Orders, values);

        if (orderId != null && orderId > 0)
        {
            List<object[]> valuesList = new();
            foreach(var product in order.Products)
            {
                values = new object[] { orderId, product.ProductId, product.Quantity, product.TotalAmount };
                valuesList.Add(values);
            }
            
            bool success = DbManager.Insert(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.OrderProducts), Tabels.OrderProducts, valuesList);
            return success ? Ok() : BadRequest();
        }
        return BadRequest();        
    }
}
