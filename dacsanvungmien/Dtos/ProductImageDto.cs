using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }
    }
    public class CreateProductImageDto
    {
        public List<IFormFile> Image { get; set; }
        public int ProductId { get; set; }
    }
}
