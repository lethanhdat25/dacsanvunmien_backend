using System;
using System.Collections.Generic;

#nullable disable

namespace dacsanvungmien.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountPassword { get; set; }
        public string UserAddress { get; set; }
        public string FaceBook { get; set; }
        public string Gmail { get; set; }
        public string Role { get; set; }
    }
}
