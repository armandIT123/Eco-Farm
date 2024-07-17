using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public enum LoggerMessageType
    {
        Info = 0, Warn = 1, Error = 2
    }
    public class Logger
    {
        public static void Insert(string message, LoggerMessageType messageType, string location)
        {
            object[] values = {0 , message, messageType, location, DateTime.UtcNow };
            DbManager.InsertAndReturnId("", nameof(Tabels.Logger), Tabels.Logger, values);
        }
    }
}
