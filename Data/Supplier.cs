﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Supplier
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public byte[]? Image { get; set; } // temporary
    public DateTime? RegisterDate { get; set; }
    public double Rating { get; set; }
}

public class SupplierAbout
{
    public string? Description { get; set; }
    public byte[][]? Images {  get; set; }
}

public class Product
{
    public int Id { get; set; }
    public int SupplierId {  get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price {  get; set; }
    public string? Category { get; set; }
    [IgnoreProperty]
    public byte[]? Image { get; set; }
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
