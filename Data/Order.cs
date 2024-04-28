namespace Data;

public enum OrderStatus
{
    Pending = 0,
    Picked = 1,
    Delivered = 2,
    Canceled = 3
}

public enum PaymentMethod
{
    GooglePay = 0,
    ApplePay = 1,
    Cash = 2,
    Card = 3,
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderProduct>? Products { get; set; }
    public AdressInfo? DeliveryAdress { get; set; } // saved as serialization
    public string? Message { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? Date { get; set; } = DateTime.Now;
}

public class OrderProduct
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? TotalAmount { get; set; }
}

public class AdressInfo
{
    public int Id { get; set; } // if separate table
    public int UserId { get; set; } //if separate table
    public string? Adress { get; set; }
    public string? AdditionalDetails { get; set; }
    public double? Lat { get; set; }
    public double? Lng { get; set; }

}
