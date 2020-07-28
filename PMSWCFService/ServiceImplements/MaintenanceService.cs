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
    public class MaintenanceService : IMaintenanceService
    {
        public int AddMainitenancePlan(DcMaintenancePlan model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaintenancePlan, MaintenancePlan>());
                    var plan = Mapper.Map<MaintenancePlan>(model);
                    dc.MaintenancePlans.Add(plan);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }

        }

        public int AddMainitenanceRecord(DcMaintenanceRecord model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaintenanceRecord, MaintenanceRecord>());
                    var record = Mapper.Map<MaintenanceRecord>(model);
                    dc.MaintenanceRecords.Add(record);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }

        }

        public int GetMaintenancePlanCount(string devicecode, string planitem)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.MaintenancePlans
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.VHPMachineCode.Contains(devicecode)
                                && m.PlanItem.Contains(planitem)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }


        public List<DcMaintenancePlan> GetMaintenancePlans(int s, int t, string devicecode, string planitem)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaintenancePlan, DcMaintenancePlan>());
                    var query = from m in db.MaintenancePlans
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.VHPMachineCode.Contains(devicecode)
                                && m.PlanItem.Contains(planitem)
                                orderby m.VHPMachineCode ascending
                                select m;
                    var result = query.ToList().Skip(s).Take(t);
                    return Mapper.Map<List<MaintenancePlan>, List<DcMaintenancePlan>>(result.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public List<DcMaintenanceRecord> GetMaintenanceRecords(int s, int t, string devicecode, string planitem)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaintenanceRecord, DcMaintenanceRecord>());
                    var query = from m in db.MaintenanceRecords
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.VHPMachineCode.Contains(devicecode)
                                && m.PlanItem.Contains(planitem)
                                orderby m.VHPMachineCode ascending
                                select m;
                    var result = query.ToList().Skip(s).Take(t);
                    return Mapper.Map<List<MaintenanceRecord>, List<DcMaintenanceRecord>>(result.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int GetMaintenanceRecordsCount(string devicecode, string planitem)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.MaintenanceRecords
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.VHPMachineCode.Contains(devicecode)
                                && m.PlanItem.Contains(planitem)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int UpdateMainitenancePlan(DcMaintenancePlan model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaintenancePlan, MaintenancePlan>());
                    var plan = Mapper.Map<MaintenancePlan>(model);
                    dc.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }

        }

        public int UpdateMainitenanceRecord(DcMaintenanceRecord model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaintenanceRecord, MaintenanceRecord>());
                    var record = Mapper.Map<MaintenanceRecord>(model);
                    dc.Entry(record).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }

        }
    }
}