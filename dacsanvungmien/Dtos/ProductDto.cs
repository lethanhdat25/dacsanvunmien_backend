using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? PriceSale { get; set; }
        public string Dvt { get; set; }
        public int Amount { get; set; }
        public string  Weight { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
    }
    public class CreateProductDto
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? PriceSale { get; set; }
        public string Dvt { get; set; }
        public int Amount { get; set; }
        public string Weight { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }

    }
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public string Dvt { get; set; }
        public int Amount { get; set; }
        public string Weight { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }

    }
}
