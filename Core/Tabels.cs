namespace Core;

internal class Tabels
{
    public static readonly string[] Users = new string[] { "Id", "Name", "PhoneNo", "Email", "Password", "Salt", "RegisterDate" };

    public static readonly string[] Suppliers = new string[] { "Id", "Name", "Rating", "RegisterDate" };
    public static readonly string[] Reviews = new string[] { "Id", "SupplierId", "UserId", "UserFullName", "Stars", "Date" };
    public static readonly string[] Products = new string[] { "Id", "SupplierId", "Name", "Description", "Price", "Category" };
    public static readonly string[] Orders = new string[] { "Id", "UserId", "DeliveryAdress", "Message", "PaymentMethod", "Status", "Date" };
    public static readonly string[] OrderProducts = new string[] { "Id", "OrderId", "ProductId", "Quantity", "TotalAmount" };


    public static readonly string[] Logger = new string[] { "UserId", "Message", "MessageType", "Location", "Date" };

}
