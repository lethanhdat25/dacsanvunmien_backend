using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}
