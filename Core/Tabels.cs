﻿namespace Core;

internal class Tabels
{
    public static readonly string[] Users1 = new string[] { "Email", "Name", "Surname", "Password" };

  
    public static string[] Users(bool post = false, bool get = false)
    {
        List<string> columns = new List<string>{"Email", "Name", "Surname"};
        if (post)
            columns.Add("Password");
        else if (get)
            columns.Insert(0, "Id");

        return columns.ToArray();
    }

    public static readonly string[] Suppliers = new string[] { "Id", "Name", "Rating", "RegisterDate" };
    public static readonly string[] Reviews = new string[] { "Id", "SupplierId", "UserId", "UserFullName", "Stars", "Date" };
    public static readonly string[] Products = new string[] { "Id", "SupplierId", "Name", "Description", "Price", "Category" };
    public static readonly string[] Orders = new string[] { "Id", "UserId", "DeliveryAdress", "Message", "PaymentMethod", "Status", "Date" };
    public static readonly string[] OrderProducts = new string[] { "Id", "OrderId","ProductId", "Quantity", "TotalAmount" };


    public static readonly string[] Logger = new string[] { "UserId", "Message", "MessageType", "Location", "Date" };
   
}
