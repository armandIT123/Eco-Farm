namespace Core;

public class Tabels
{
    public static readonly string[] Users1 = new string[] { "Email", "Name", "Surname", "Password" };

  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fullData"></param>
    /// <param name="forInsert"></param>
    /// <returns></returns>
    public static string[] Users(bool post = false, bool get = false)
    {
        List<string> columns = new List<string>{"Email", "Name", "Surname"};
        if (post)
            columns.Add("Password");
        else if (get)
            columns.Insert(0, "Id");

        return columns.ToArray();
    }

    public static readonly string[] Suppliers = new string[] { "Id", "Name", "RegisterDate" };
    public static readonly string[] Reviews = new string[] { "Id", "SupplierId", "UserId", "UserFullName", "Stars", "Date" };


    public static readonly string[] Logger = new string[] { "UserId", "Message", "MessageType", "Location", "Date" };
   
}
