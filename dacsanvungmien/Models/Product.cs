﻿using System;
using System.Collections.Generic;

#nullable disable

namespace dacsanvungmien.Models
{
    public record Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Region Region { get; set; }
    }
}