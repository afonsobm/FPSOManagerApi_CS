using System;
using System.Collections.Generic;
using FPSOManagerApi_CS.Models;

namespace FPSOManagerApi_CS.DTO
{
    public class VesselDto 
    {
        public String code { get; set; }
        public List<Equipment> equipments { get; set; }
    }
}