using Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public SupplierController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet(Name = "GetSuppliers")]
    public IActionResult GetSuppliers()
    {
        var rawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Suppliers), Tabels.Suppliers);

        if (rawData == null || rawData.Count == 0)
            return NotFound();

        List<Supplier> suppliers = new();

        foreach(var data in rawData)
        {
            try
            {
                int pos = 0;
                var supplier = new Supplier()
                {
                    Id = Convert.ToInt32(data[pos++]),
                    Name = data[pos++] as string,
                    Rating = Convert.ToDouble(data[pos++]),
                    RegisterDate = Convert.ToDateTime(data[pos++]),
                };
                string imagePath = $"C:\\Data\\Suppliers-Images\\{100 + supplier.Id}\\main.jpeg";
                supplier.Image = Tools.ImageToByteArray(imagePath);
                suppliers.Add(supplier);
            }
            catch (Exception ex)
            {
                // logg exception
            }
        }

        return Ok(suppliers);
    }

    [Route("GetDescription")]
    [HttpGet]
    public IActionResult GetSupplierDescription(int id)
    {
        if (!(id > 0))
            return BadRequest("Invalid Id");

        string sqlFilter = $"Id={id}";
        var rawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Suppliers), new string[] { "Description" }, sqlFilter);

        if (rawData == null || rawData.Count == 0)
            return NotFound();

        try
        {
            SupplierAbout supplierAbout = new SupplierAbout() { Description = rawData[0][0] as string };

            string imagesPath = $"C:\\Data\\Suppliers-Images\\{100 + id}\\About";
            if (Directory.Exists(imagesPath))
            {
                var images = Directory.GetFiles(imagesPath);
                List<byte[]> imageList = new List<byte[]>();
                foreach (var image in images)
                {
                    imageList.Add(Tools.ImageToByteArray(image));
                }
                supplierAbout.Images = imageList.ToArray();
            }

            return Ok(supplierAbout);
        }
        catch (Exception ex)
        {
            return BadRequest();
            //logger
        }
    }

}
