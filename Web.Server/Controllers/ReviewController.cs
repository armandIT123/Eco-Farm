using Data;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ReviewController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet(Name = "GetReviews")]
    public IActionResult GetSuppliers(int supplierId)
    {
        if (!(supplierId > 0))
            return BadRequest();

        string sqlFilter = $"SupplierId={supplierId}";
        string orderBy = "Date ASC";
        var rawData = DbManager.Select(_configuration.GetConnectionString("SqlServerDb") ?? "", nameof(Tabels.Reviews), Tabels.Reviews, sqlFilter, orderBy);

        if (rawData == null || rawData.Count == 0)
            return NotFound();

        List<Review> reviews = new();

        foreach (var data in rawData)
        {
            try
            {
                int pos = 0;
                Review review = new Review()
                {
                    Id = Convert.ToInt32(data[pos++]),
                    SupplierId = Convert.ToInt32(data[pos++]),
                    UserId = Convert.ToInt32(data[pos++]),
                    UserFullName = data[pos++] as string,
                    Stars = Convert.ToInt32(data[pos++]),
                    Date = Convert.ToDateTime(data[pos++]),
                };
                reviews.Add(review);
            }
            catch (Exception ex)
            {
                // logger
            }
        }
        return Ok(reviews);
    }

}
