﻿namespace Buisness.Models;

public class ProductUpdateForm
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal? Price { get; set; }
}
