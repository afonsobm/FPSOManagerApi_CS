using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FPSOManagerApi_CS.Models;
using FPSOManagerApi_CS.Services;
using FPSOManagerApi_CS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FPSOController : ControllerBase
    {
        private readonly ILogger<FPSOController> _logger;
        private readonly FPSOServices _FPSOServices;

        public FPSOController(ILogger<FPSOController> logger, FPSOServices FPSOServices)
        {
            _logger = logger;
            _FPSOServices = FPSOServices;
        }


        /// <summary>
        /// Registers a vessel.
        /// </summary>
        /// <remarks>
        /// Sample request: POST /FPSO/vessel
        ///     
        /// </remarks>
        /// <param name="vesselCode"></param>
        /// <returns>A newly registered Vessel</returns>
        /// <response code="201">Vessel Registered Successfully</response>
        /// <response code="409">Vessel Already Registered</response>   
        [ProducesResponseType(typeof(Vessel), 201)]
        [ProducesResponseType(typeof(String), 409)]
        [HttpPost("vessel")]
        public IActionResult PostVessel(string vesselCode)
        {
            try
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                Vessel vessel = _FPSOServices.InsertVessel(vesselCode);

                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - SUCCESS", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return Created("", vessel);
            }
            catch (BusinessException ex)
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - FAIL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return StatusCode((int)ex.statusCode, ex.message);
            }
        }

        /// <summary>
        /// Registers an equipment to a registered vessel.
        /// </summary>
        /// <remarks>
        /// Sample request: POST /FPSO/equipment
        ///     
        ///     {
        ///         "name": "name_example",
        ///         "code": "code_example",
        ///         "location": "location_example"
        ///     }
        ///     
        /// </remarks>
        /// <param name="vesselCode"></param>
        /// <param name="equipment"></param>
        /// <returns>A newly registered equipment</returns>
        /// <response code="201">Equipment Registered Successfully</response>
        /// <response code="404">Vessel Not Registered</response>   
        /// <response code="409">Equipment Already Registered</response>   
        [ProducesResponseType(typeof(Equipment), 201)]
        [ProducesResponseType(typeof(String), 404)]
        [ProducesResponseType(typeof(String), 409)]
        [HttpPost("equipment")]
        public IActionResult PostEquipment(string vesselCode, [FromBody] Equipment equipment)
        {
            try
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                equipment.Vesselcode = vesselCode;
                Equipment insertedEquipment = _FPSOServices.InsertEquipment(equipment);

                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - SUCCESS", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return Created("", insertedEquipment);
            }
            catch (BusinessException ex)
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - FAIL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return StatusCode((int)ex.statusCode, ex.message);
            }
        }

        /// <summary>
        /// Deactivates a list of equipment
        /// </summary>
        /// <remarks>
        /// Sample request: PUT /FPSO/equipment
        ///     
        ///     [
        ///         "equipment_code_example_1",
        ///         "equipment_code_example_2",
        ///         "equipment_code_example_1"
        ///     ]
        ///     
        /// </remarks>
        /// <param name="codes"></param>
        /// <response code="204">Equipments deactivated</response>
        /// <response code="404">Equipments Not Registered</response>   
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(String), 404)]
        [HttpPut("equipment")]
        public IActionResult PutInactiveEquipment([FromBody] List<String> codes)
        {
            try
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                _FPSOServices.UpdateEquipmentsToInactive(codes);

                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - SUCCESS", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return NoContent();
            }
            catch (BusinessException ex)
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - FAIL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return StatusCode((int)ex.statusCode, ex.message);
            }
        }

        /// <summary>
        /// Retrieves all the active equipment in a vessel
        /// </summary>
        /// <remarks>
        /// Sample request: GET /FPSO/vessel
        ///     
        /// </remarks>
        /// <param name="vesselCode"></param>
        /// <returns>All the active equipments in a vessel</returns>
        /// <response code="200">Found Equipments from Vessel</response>
        /// <response code="404">Vessel Not Registered</response>   
        [ProducesResponseType(typeof(List<Equipment>), 200)]
        [ProducesResponseType(typeof(String), 404)]
        [HttpGet("vessel")]
        public IActionResult GetVesselEquipment(String vesselCode)
        {   
            try
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Controller", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                List<Equipment> activeEquipments = _FPSOServices.GetEquipmentsFromVessel(vesselCode);

                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - SUCCESS", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return Ok(activeEquipments);
            }
            catch (BusinessException ex)
            {
                _logger.LogInformation("{0} | {1} | {2} | {3} | End Controller - FAIL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return StatusCode((int)ex.statusCode, ex.message);
            }
        }

        
    }
}
