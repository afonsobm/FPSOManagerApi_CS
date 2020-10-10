using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPSOManagerApi_CS.Models
{
    public class Equipment
    {
        public String name { get; set; }
        public String code { get; set; }
        public String location { get; set; }
        public Boolean active { get; set; }
    }
}
