using System;
using System.Linq;
using System.Reflection;
using FPSOManagerApi_CS.DTO;
using FPSOManagerApi_CS.Models;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.DAL
{
    public class VesselDal
    {
        private readonly ILogger<VesselDal> _logger;
        private readonly FPSODbContext _dbContext;

        public VesselDal(ILogger<VesselDal> logger, FPSODbContext dbContext)
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

        public Vessel InsertVessel(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel vessel = new Vessel { code = vesselCode };
            _dbContext.Add<Vessel>(vessel);
            _dbContext.SaveChanges();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End DAL", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return vessel;
        }
    }
}