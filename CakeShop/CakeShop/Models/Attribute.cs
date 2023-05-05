using System;
using System.Collections.Generic;

namespace CakeShop.Models;

public partial class Attribute
{
    public int AttributeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AttributesPrice> AttributesPrices { get; set; } = new List<AttributesPrice>();
}
