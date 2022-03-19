using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public virtual Account User { get; set; }
    }
}
