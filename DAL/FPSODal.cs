using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FPSOManagerApi_CS.Models;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.DAL
{
    public class FPSODal
    {
        private readonly ILogger<FPSODal> _logger;
        private readonly FPSODbContext _dbContext;

        public FPSODal(ILogger<FPSODal> logger, FPSODbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public Vessel GetVessel(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return _dbContext.vessels.Where(v => v.code.Equals(vesselCode)).FirstOrDefault();
        }

        public Equipment GetEquipment(string equipmentCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return _dbContext.equipment.Where(v => v.code.Equals(equipmentCode)).FirstOrDefault();
        }

        public List<Equipment> GetEquipments(List<String> equipmentCodes)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return _dbContext.equipment.Where(v => equipmentCodes.Contains(v.code))?.ToList();
        }

        public List<Equipment> GetActiveEquipments(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return _dbContext.equipment.Where(v => v.Vesselcode.Equals(vesselCode) && v.active.Equals(true))?.ToList();
        }

        public Vessel InsertVessel(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel vessel = new Vessel { code = vesselCode };
            _dbContext.Add<Vessel>(vessel);
            _dbContext.SaveChanges();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return vessel;
        }

        public Equipment InsertEquipment(Equipment equipment)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            _dbContext.Add<Equipment>(equipment);
            _dbContext.SaveChanges();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return equipment;
        }

        public void UpdateEquipments(List<Equipment> equipments)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            _dbContext.UpdateRange(equipments);
            _dbContext.SaveChanges();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
        }
    }
}