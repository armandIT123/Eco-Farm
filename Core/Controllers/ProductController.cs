using Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ProductController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet(Name = "GetProducts")]
    public IActionResult GetProducts(int supplierId)
    {
        if (!(supplierId > 0))
            return BadRequest();

        string sqlFilter = $"SupplierId={supplierId}";
        var rawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Products), Tabels.Products, sqlFilter);

        if(rawData == null ||  rawData.Count == 0)
            return NotFound();

        List<Product> products = new();
        foreach(var data in rawData)
        {
            int pos = 0;
            try
            {
                var product = new Product()
                {
                    Id = Convert.ToInt32(data[pos++]),
                    SupplierId = Convert.ToInt32(data[pos++]),
                    Name = data[pos++] as string,
                    Description = data[pos++] as string,
                    Price = Convert.ToDouble(data[pos++]),
                    Category = data[pos] as string,
                };

                string imagePath = $"C:\\Data\\Suppliers-Images\\{100 + supplierId}\\Products\\{product.Id}.jpg";
                product.Image = Tools.ImageToByteArray(imagePath);

                products.Add(product);
            }
            catch(Exception ex)
            {
                //logger
            }
        }
        return Ok(products);
    }
}
