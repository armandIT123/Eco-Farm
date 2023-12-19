using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Supplier
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? RegisterDate { get; set; }
    public int Rating { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}

public class Review
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public int UserId { get; set; }
    public string? UserFullName { get; set; }
    public int Stars { get; set; }
    public DateTime? Date { get; set; }
}

public class Filters
{

}
