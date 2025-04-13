using System;
using System.Collections.Generic;

namespace FoodProducts.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte GroupId { get; set; }

    public byte PackagingTypeId { get; set; }

    public virtual ProductGroup Group { get; set; } = null!;

    public virtual ProductPackagingType PackagingType { get; set; } = null!;

    public virtual ICollection<ProductOutput> ProductOutputs { get; set; } = new List<ProductOutput>();
}
