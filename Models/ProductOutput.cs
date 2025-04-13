using System;
using System.Collections.Generic;

namespace FoodProducts.Models;

public partial class ProductOutput
{
    public short CompanyId { get; set; }

    public int ProductId { get; set; }

    public int ProductAmount { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
