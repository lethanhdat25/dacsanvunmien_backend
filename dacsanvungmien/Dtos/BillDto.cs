using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class BillDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
    public class CreateBillDto
    {
        public decimal Total { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
