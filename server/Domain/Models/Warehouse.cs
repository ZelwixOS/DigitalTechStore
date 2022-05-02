﻿namespace Domain.Models
{
    using System.Collections.Generic;

    public class Warehouse : EstateUnit
    {
        public HashSet<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
