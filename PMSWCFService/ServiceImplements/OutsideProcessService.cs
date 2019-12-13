using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public class OutsideProcessService : IOutsideProcessService
    {
        public int Add(DcOutsideProcess model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOutsideProcess, OutsideProcess>());
                    var entity = Mapper.Map<OutsideProcess>(model);
                    dc.OutsideProcesses.Add(entity);
                    dc.SaveChanges();
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public List<DcOutsideProcess> GetOutsideProcess(int s, int t, string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OutsideProcess, DcOutsideProcess>());
                    var query = from i in dc.OutsideProcesses
                                where i.State != PMSCommon.OutsideProcessState.作废.ToString()
                                && i.ProductID.Contains(productid)
                                && i.Composition.Contains(composition)
                                orderby i.ProductID descending,i.CreateTime descending
                                select i;
                    return Mapper.Map<List<OutsideProcess>, List<DcOutsideProcess>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public int GetOutsideProcessCount(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.OutsideProcesses
                                where i.State != PMSCommon.OutsideProcessState.作废.ToString()
                                && i.ProductID.Contains(productid)
                                && i.Composition.Contains(composition)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public List<DcOutsideProcess> GetOutsideProcessUnCompleted(int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OutsideProcess, DcOutsideProcess>());
                    var query = from i in dc.OutsideProcesses
                                where i.State == PMSCommon.OutsideProcessState.待发出.ToString()
                                orderby  i.ProductID descending,i.CreateTime descending
                                select i;
                    return Mapper.Map<List<OutsideProcess>, List<DcOutsideProcess>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public List<DcOutsideProcess> GetOutsideProcessUnCompletedBack(int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OutsideProcess, DcOutsideProcess>());
                    var query = from i in dc.OutsideProcesses
                                where i.State == PMSCommon.OutsideProcessState.已发出.ToString()
                                orderby i.ProductID descending, i.CreateTime descending
                                select i;
                    return Mapper.Map<List<OutsideProcess>, List<DcOutsideProcess>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public int GetOutsideProcessUnCompletedBackCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.OutsideProcesses
                                where i.State == PMSCommon.OutsideProcessState.已发出.ToString()
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public int GetOutsideProcessUnCompletedCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.OutsideProcesses
                                where i.State == PMSCommon.OutsideProcessState.待发出.ToString()
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }

        public int Update(DcOutsideProcess model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOutsideProcess, OutsideProcess>());
                    var entity = Mapper.Map<OutsideProcess>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw;
            }
        }
    }
}