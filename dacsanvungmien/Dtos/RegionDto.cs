using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dacsanvungmien.Dtos
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateRegionDto
    {
        public string Name { get; set; }
    }
}
