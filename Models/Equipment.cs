using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPSOManagerApi_CS.Models
{
    public class Equipment
    {
        [ForeignKey("vesselCode")]
        public String Vesselcode { get; set; }
        public String name { get; set; }
        public String code { get; set; }
        public String location { get; set; }
        public Boolean active { get; set; }
    }
}
