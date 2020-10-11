using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using FPSOManagerApi_CS.DAL;
using FPSOManagerApi_CS.Models;
using FPSOManagerApi_CS.Utils;
using Microsoft.Extensions.Logging;

namespace FPSOManagerApi_CS.Services
{
    public class FPSOServices
    {
        private readonly ILogger<FPSOServices> _logger;

        private readonly FPSODal _FPSODal;

        public FPSOServices(ILogger<FPSOServices> logger, FPSODal FPSODal)
        {
            _logger = logger;
            _FPSODal = FPSODal;
        }

        public Vessel InsertVessel(string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking for existing vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel vessel = _FPSODal.GetVessel(vesselCode);

            if (vessel != null)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Vessel Already Registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Vessel Already Registered", HttpStatusCode.Conflict);
            }
            
            _logger.LogInformation("{0} | {1} | {2} | {3} | Inserting vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            _FPSODal.BeginTransaction();
            vessel = _FPSODal.InsertVessel(vesselCode);
            _FPSODal.CommitTransaction();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return vessel;
        }

        public Equipment InsertEquipment(Equipment equipment)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking for existing vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel checkVessel = _FPSODal.GetVessel(equipment.Vesselcode);

            if (checkVessel == null)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Vessel Is Not Registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Vessel Is Not Registered", HttpStatusCode.NotFound);
            }

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking for existing equipment", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Equipment checkEquip = _FPSODal.GetEquipment(equipment.code);

            if (checkEquip != null)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Equipment Already Registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Equipment Already Registered", HttpStatusCode.Conflict);
            }
            
            _logger.LogInformation("{0} | {1} | {2} | {3} | Inserting Equipment", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            equipment.active = true;
            _FPSODal.BeginTransaction();
            equipment = _FPSODal.InsertEquipment(equipment);
            _FPSODal.CommitTransaction();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            return equipment;
        }

        public void UpdateEquipmentsToInactive(List<String> codes)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking if equipments are registered", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            List<Equipment> equipments = _FPSODal.GetEquipments(codes);

            if (equipments == null || equipments.Count < codes.Count)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Some of the equipment codes are not registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Some of the equipment codes are not registered", HttpStatusCode.NotFound);
            }

            _logger.LogInformation("{0} | {1} | {2} | {3} | Deactivating equipments", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            equipments.ForEach(e => e.active = false);
            _FPSODal.BeginTransaction();
            _FPSODal.UpdateEquipments(equipments);
            _FPSODal.CommitTransaction();

            _logger.LogInformation("{0} | {1} | {2} | {3} | End Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
        }

        public List<Equipment> GetEquipmentsFromVessel (string vesselCode)
        {
            _logger.LogInformation("{0} | {1} | {2} | {3} | Begin Service", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation("{0} | {1} | {2} | {3} | Checking for existing vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            Vessel checkVessel = _FPSODal.GetVessel(vesselCode);

            if (checkVessel == null)
            {
                _logger.LogError("{0} | {1} | {2} | {3} | Vessel Is Not Registered", DateTime.Now, "ERROR", this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                throw new BusinessException("Vessel Is Not Registered", HttpStatusCode.NotFound);
            }

            _logger.LogInformation("{0} | {1} | {2} | {3} | Retrieving active equipment from vessel", DateTime.Now, "INFO", this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            return _FPSODal.GetActiveEquipments(vesselCode);
        }
    }
}