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
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int AddMainitenanceRecord(DcMaintenanceRecord model)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int DeleteMainitenancePlan(Guid id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int DeleteMainitenanceRecord(Guid id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int GetMaintenancePlanCount()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcMaintenancePlan> GetMaintenancePlans(int skip, int take)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public List<DcMaintenanceRecord> GetMaintenanceRecords(Guid planid)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateMainitenancePlan(DcMaintenancePlan model)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }

        public int UpdateMainitenanceRecord(DcMaintenanceRecord model)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }

        }
    }
}