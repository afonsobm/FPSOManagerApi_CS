using System.Collections.Generic;
using System.Linq;
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

            _dbContext.Add<Vessel>(new Vessel{ code = "2"});
            _dbContext.Add<Equipment>(new Equipment{ Vesselcode = "2", name = "name2", code = "code2", location = "loc2", active = true});
            _dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Vessel vessel = new Vessel {code = "1"};
            _dbContext.Add<Vessel>(vessel);
            _dbContext.SaveChanges();

            Vessel vessel2 = _dbContext.vessels.Where(v => v.code.Equals("1")).FirstOrDefault();
            Assert.AreEqual(vessel.code, vessel2.code);
        }

        [TestMethod]
        public void GetEquipment()
        {
            var loggerMockDal = new Mock<ILogger<FPSODal>>();
            FPSODal fpsoDal = new FPSODal(loggerMockDal.Object, _dbContext);

            var loggerMockService = new Mock<ILogger<FPSOServices>>();
            FPSOServices fpsoServices = new FPSOServices(loggerMockService.Object, fpsoDal);

            var loggerMockController = new Mock<ILogger<FPSOController>>();
            FPSOController fpsoController = new FPSOController(loggerMockController.Object, fpsoServices);

            IActionResult result = fpsoController.GetVesselEquipment("2");
            OkObjectResult objectResult = result as OkObjectResult;

            List<Equipment> equipments = objectResult.Value as List<Equipment>;
            
            Assert.AreEqual(objectResult.StatusCode, 200);
            Assert.AreEqual(equipments[0].name, "name2");
            Assert.AreEqual(equipments[0].code, "code2");
            Assert.AreEqual(equipments[0].location, "loc2");
            Assert.AreEqual(equipments[0].active, true);
        }
    }
}
