using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FPSOManagerApi_CS.Controllers;
using FPSOManagerApi_CS.DAL;
using FPSOManagerApi_CS.Models;
using FPSOManagerApi_CS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ProjectTests
{
    [TestClass]
    public class FPSOUnitTests
    {
        private FPSODbContext _dbContext;
        private FPSOController _fpsoController;
        private FPSOServices _fpsoServices;
        private FPSODal _fpsoDal;

        [TestInitialize]
        public void TestInitialize()
        {
            var connection = new SqliteConnection("DataSource=:memory:");  
            connection.Open(); 

            var options = new DbContextOptionsBuilder<FPSODbContext>()
                                .UseSqlite(connection).Options;

            _dbContext = new FPSODbContext(options);

            if (_dbContext != null)  
            {  
                _dbContext.Database.EnsureDeleted();  
                _dbContext.Database.EnsureCreated();  
            }  

            _dbContext.Add<Vessel>(new Vessel{ code = "1"});
            _dbContext.Add<Equipment>(new Equipment{ Vesselcode = "1", name = "name1", code = "code1", location = "loc1", active = true});
            _dbContext.Add<Vessel>(new Vessel{ code = "2"});
            _dbContext.Add<Equipment>(new Equipment{ Vesselcode = "2", name = "name2", code = "code2", location = "loc2", active = true});
            _dbContext.SaveChanges();


            var loggerMockDal = new Mock<ILogger<FPSODal>>();
            _fpsoDal = new FPSODal(loggerMockDal.Object, _dbContext);

            var loggerMockService = new Mock<ILogger<FPSOServices>>();
            _fpsoServices = new FPSOServices(loggerMockService.Object, _fpsoDal);

            var loggerMockController = new Mock<ILogger<FPSOController>>();
            _fpsoController = new FPSOController(loggerMockController.Object, _fpsoServices);
        }

        [TestMethod]
        [DataRow("2")]
        public void GetVesselEquipment_Success(string vesselCode)
        {
            IActionResult result = _fpsoController.GetVesselEquipment(vesselCode);
            OkObjectResult objectResult = result as OkObjectResult;

            List<Equipment> equipments = objectResult.Value as List<Equipment>;
            
            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreEqual(equipments[0].name, "name2");
            Assert.AreEqual(equipments[0].code, "code2");
            Assert.AreEqual(equipments[0].location, "loc2");
            Assert.AreEqual(equipments[0].active, true);
        }

        [TestMethod]
        [DataRow("3")]
        public void GetVesselEquipment_UnregisteredVessel(string vesselCode)
        {
            IActionResult result = _fpsoController.GetVesselEquipment(vesselCode);
            ObjectResult objectResult = result as ObjectResult;
            
            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        [DataRow("code1")]
        public void PutInactiveEquipment_Success(string equipmentCode)
        {
            List<String> listEquip = new List<string> {equipmentCode};
            IActionResult result = _fpsoController.PutInactiveEquipment(listEquip);
            NoContentResult objectResult = result as NoContentResult;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [TestMethod]
        [DataRow("dummy")]
        public void PutInactiveEquipment_UnregisteredEquipment(string equipmentCode)
        {
            List<String> listEquip = new List<string> {equipmentCode};
            IActionResult result = _fpsoController.PutInactiveEquipment(listEquip);
            ObjectResult objectResult = result as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        [DataRow("4")]
        public void PostVessel_Success(string vesselCode)
        {
            IActionResult result = _fpsoController.PostVessel(vesselCode);
            CreatedResult objectResult = result as CreatedResult;

            Vessel retVessel = objectResult.Value as Vessel;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.Created);
            Assert.AreEqual(retVessel.code, vesselCode);
        }

        [TestMethod]
        [DataRow("2")]
        public void PostVessel_VesselAlreadyRegistered(string vesselCode)
        {
            IActionResult result = _fpsoController.PostVessel(vesselCode);
            ObjectResult objectResult = result as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.Conflict);
        }

        [TestMethod]
        [DataRow("1", "newName", "newCode", "newLoc")]
        public void PostEquipment_Success(string vesselCode, string eqName, string eqCode, string eqLocation)
        {
            Equipment equip = new Equipment {name = eqName, code = eqCode, location = eqLocation};

            IActionResult result = _fpsoController.PostEquipment(vesselCode, equip);
            CreatedResult objectResult = result as CreatedResult;

            Equipment retEquip = objectResult.Value as Equipment;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.Created);
            Assert.AreEqual(retEquip.name, eqName);
            Assert.AreEqual(retEquip.code, eqCode);
            Assert.AreEqual(retEquip.location, eqLocation);
            Assert.AreEqual(retEquip.active, true);
        }

        [TestMethod]
        [DataRow("dummy", "newName", "newCode", "newLoc")]
        public void PostEquipment_UnregisteredVessel(string vesselCode, string eqName, string eqCode, string eqLocation)
        {
            Equipment equip = new Equipment {name = eqName, code = eqCode, location = eqLocation};

            IActionResult result = _fpsoController.PostEquipment(vesselCode, equip);
            ObjectResult objectResult = result as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        [DataRow("1", "newName", "code1", "newLoc")]
        public void PostEquipment_EquipmentAlreadyRegistered(string vesselCode, string eqName, string eqCode, string eqLocation)
        {
            Equipment equip = new Equipment {name = eqName, code = eqCode, location = eqLocation};

            IActionResult result = _fpsoController.PostEquipment(vesselCode, equip);
            ObjectResult objectResult = result as ObjectResult;

            Assert.AreEqual(objectResult.StatusCode, (int)HttpStatusCode.Conflict);
        }
    }
}
