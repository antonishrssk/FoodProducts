using System;
using System.Collections.Generic;

namespace FoodProducts.Models;

public partial class Company
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string DirectorFio { get; set; } = null!;

    public virtual ICollection<ProductOutput> ProductOutputs { get; set; } = new List<ProductOutput>();
}
