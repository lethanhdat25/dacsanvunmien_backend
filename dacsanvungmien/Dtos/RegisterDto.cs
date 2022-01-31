using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountPassword { get; set; }
        public string UserAddress { get; set; }
        public string Gmail { get; set; }
        public string Role { get; set; }
    }
}
