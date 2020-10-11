using System;
using System.Net;
using System.Reflection;
using FPSOManagerApi_CS.DAL;
using FPSOManagerApi_CS.DTO;
using FPSOManagerApi_CS.Models;
using FPSOManagerApi_CS.Utils;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.Services
{
    public class VesselServices
    {
        private readonly ILogger<VesselServices> _logger;

        private readonly VesselDal _vesselDal;

        public VesselServices(ILogger<VesselServices> logger, VesselDal vesselDal)
        {
            _logger = logger;
            _vesselDal = vesselDal;
        }

        public VesselDto InsertVessel(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking for existing vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel vessel = _vesselDal.GetVessel(vesselCode);

            if (vessel != null)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Vessel Already Registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Vessel Already Registered", HttpStatusCode.UnprocessableEntity);
            }
            
            _logger.LogInformation("{0} | {1} | {2} | {3} | Inserting vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            _vesselDal.BeginTransaction();
            vessel = _vesselDal.InsertVessel(vesselCode);
            _vesselDal.CommitTransaction();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return vessel;
        }
    }
}