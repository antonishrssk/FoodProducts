using System;
using System.Collections.Generic;

namespace FoodProducts.Models;

public partial class ProductPackagingType
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
