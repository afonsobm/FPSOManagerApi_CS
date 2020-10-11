using System;
using FPSOManagerApi_CS.Models;

namespace FPSOManagerApi_CS.DTO
{
    public class EquipmentDto
    {
        public String name { get; set; }
        public String code { get; set; }
        public String location { get; set; }
        public Boolean active { get; set; }
    }
}