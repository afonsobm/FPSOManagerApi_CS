using System;
using FPSOManagerApi_CS.DAL;
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

        public String getTest() { return "test"; }
    }
}