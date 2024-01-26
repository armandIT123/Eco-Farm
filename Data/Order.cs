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
    Cash = 0,
    Card = 1,
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderProduct>? Products { get; set; }
    public Adress? DeliveryAdress { get; set; } // saved as serialization
    public string? Message {  get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? Date { get; set; } = DateTime.Now;
}

public class OrderProduct
{
    public int Id { get; set; }
    public int OrderId {  get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? TotalAmount { get; set;}
}

public class Adress
{
    public string? County { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? StreetNo { get; set; }
    public string? Flat { get; set; }
}
