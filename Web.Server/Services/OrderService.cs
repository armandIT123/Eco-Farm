using Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Core.Services;

public interface IOrderService
{
    public bool PlaceOrder(Order order);
    public List<Order> GetAllOrders(int userId);
    public Order GetOrderById(int userId, int id);
}

public class OrderService : IOrderService
{
    private readonly IConfiguration _configuration;
    public OrderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Order> GetAllOrders(int userId)
    {
        string sqlFilter = $"UserId={userId}";
        string orderBy = "Date DESC";
        var orderRawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Orders), Tabels.Orders, sqlFilter, orderBy);

        if (orderRawData == null || orderRawData.Count == 0)
            return null;

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
                    DeliveryAdress = JsonSerializer.Deserialize<AdressInfo>(data[pos++] as string),
                    Message = data[pos++] as string,
                    PaymentMethod = (PaymentMethod)Convert.ToInt32(data[pos++]),
                    Status = (OrderStatus)Convert.ToInt32(data[pos++]),
                    Date = Convert.ToDateTime(data[pos++]),
                    Products = new()
                };

                sqlFilter = $"OrderId={order.Id}";
                var productsRawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.OrderProducts), Tabels.OrderProducts, sqlFilter);

                if (productsRawData == null || productsRawData.Count == 0)
                    return null;

                foreach (var product in productsRawData)
                {
                    pos = 0;
                    try
                    {
                        // OrderProduct
                    }
                    catch (Exception ex)
                    {

                    }
                }

                orders.Add(order);
            }
            catch (Exception ex)
            {
                //logger

            }
        }

        var orderIds = orders.Select(x => x.Id).ToList();

        return orders;
    }

    public Order GetOrderById(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public bool PlaceOrder(Order order)
    {
        object[] values = { order.UserId, JsonSerializer.Serialize(order.DeliveryAdress), order.Message, order.PaymentMethod, (int)OrderStatus.Pending, DateTime.Now };
        int? orderId = DbManager.InsertAndReturnId(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.Orders), Tabels.Orders, values);

        if (orderId != null && orderId > 0)
        {
            List<object[]> valuesList = new();
            foreach (var product in order.Products)
            {
                values = new object[] { orderId, product.ProductId, product.Quantity, product.TotalAmount };
                valuesList.Add(values);
            }

            bool success = DbManager.Insert(_configuration.GetConnectionString("SqlServerDb"), nameof(Tabels.OrderProducts), Tabels.OrderProducts, valuesList);
            return success;
        }
        return false;
    }
}
