﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FPSOManagerApi_CS.DTO;
using FPSOManagerApi_CS.Models;
using FPSOManagerApi_CS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FPSOController : ControllerBase
    {
        private readonly ILogger<FPSOController> _logger;
        private readonly VesselServices _vesselServices;

        public FPSOController(ILogger<FPSOController> logger, VesselServices vesselServices)
        {
            _logger = logger;
            _vesselServices = vesselServices;
        }

        [HttpPost("vessel")]
        public IActionResult PostVessel(string vesselCode)
        {
            try
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            

            
            return Ok();
        }

        [HttpPost("equipment")]
        public IActionResult PostEquipment([FromBody] EquipmentDto equipment)
        {
            return Ok();
        }

        [HttpPut("equipment")]
        public IActionResult PutInactiveEquipment([FromBody] List<String> codes)
        {
            return Ok();
        }

        [HttpGet("vessel")]
        public IActionResult GetVesselEquipment(String code)
        {   
            return Ok();
        }

        
    }
}
