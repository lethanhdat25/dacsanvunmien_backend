using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }
    }
    public class CreateCartDto
    {
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }
    }
}
