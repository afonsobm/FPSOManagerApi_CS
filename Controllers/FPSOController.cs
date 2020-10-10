using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPSOManagerApi_CS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FPSOController : ControllerBase
    {
        private readonly ILogger<FPSOController> _logger;

        public FPSOController(ILogger<FPSOController> logger)
        {
            _logger = logger;
        }

        [HttpPost("vessel")]
        public IActionResult PostVessel(string vesselCode)
        {
            return Ok();
        }

        [HttpGet("vessel")]
        public IActionResult GetVessel()
        {
            Equipment e = new Equipment
            {
                name = "test",
                code = "abc123",
                location = "Brazil",
                active = true
            };

            List<Equipment> le = new List<Equipment>();
            le.Add(e);
            Vessel v = new Vessel
            {
                code = "mv102",
                equipments = le
            };
            
            
            return Ok(v);
        }

        
    }
}
