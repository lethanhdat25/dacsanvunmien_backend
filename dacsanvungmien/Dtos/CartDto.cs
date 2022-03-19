using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }
    }
    public class CreateCartDto
    {
        [Column(TypeName = "decimal(18,4)")]
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }
    }
}
