using System;
using System.Collections.Generic;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
