using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPSOManagerApi_CS.Models
{
    public class Vessel
    {
        public String code { get; set; }
        public List<Equipment> equipments { get; set; }
    }
}
