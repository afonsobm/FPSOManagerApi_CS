using System.Linq;
using FPSOManagerApi_CS.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTests
{
    [TestClass]
    public class UnitTest1
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
    }
}
