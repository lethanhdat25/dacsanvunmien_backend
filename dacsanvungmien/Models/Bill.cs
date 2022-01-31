using System;
using System.Collections.Generic;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public virtual Account User { get; set; }
    }
}
