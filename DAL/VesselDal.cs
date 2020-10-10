using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.DAL
{
    public class VesselDal
    {
        private readonly ILogger<VesselDal> _logger;

        public VesselDal(ILogger<VesselDal> logger)
        {
            _logger = logger;
        }
    }
}