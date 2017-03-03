using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSWCFService.DataContracts;

namespace PMSWCFService
{
    public partial class PMSService : IMaintenanceService
    {
        public int AddMainitenancePlan(DcMaintenancePlan model)
        {
            throw new NotImplementedException();
        }

        public int AddMainitenanceRecord(DcMaintenanceRecord model)
        {
            throw new NotImplementedException();
        }

        public int DeleteMainitenancePlan(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteMainitenanceRecord(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetMaintenancePlanCount()
        {
            throw new NotImplementedException();
        }

        public List<DcMaintenancePlan> GetMaintenancePlans(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public List<DcMaintenanceRecord> GetMaintenanceRecords(Guid planid)
        {
            throw new NotImplementedException();
        }

        public int UpdateMainitenancePlan(DcMaintenancePlan model)
        {
            throw new NotImplementedException();
        }

        public int UpdateMainitenanceRecord(DcMaintenanceRecord model)
        {
            throw new NotImplementedException();
        }
    }
}