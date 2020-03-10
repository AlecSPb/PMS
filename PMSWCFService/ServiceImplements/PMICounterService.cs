using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public partial class ExtraService : IPMICounterService
    {
        public int AddPMICounter(DcPMICounter model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPMICounter, PMICounter>());
                    var entity = Mapper.Map<PMICounter>(model);
                    dc.PMICounters.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeletePMICounter(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPMICounter, PMICounter>());
                    var model = dc.PMICounters.Find(id);
                    dc.PMICounters.Remove(model);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPMICounter> GetPMICounter(string itemGroup, string itemName, int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.PMICounters
                                where p.ItemGroup.Contains(itemGroup)
                                && p.ItemName.Contains(itemName)
                                && p.State == PMSCommon.SimpleState.正常.ToString()
                                orderby p.RowOrder descending
                                select p;
                    var result = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<PMICounter, DcPMICounter>());
                    var models = Mapper.Map<List<PMICounter>, List<DcPMICounter>>(result);
                    return models;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPMICounterCount(string itemGroup, string itemName)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.PMICounters
                                where p.ItemGroup.Contains(itemGroup)
                                && p.ItemName.Contains(itemName)
                                && p.State == PMSCommon.SimpleState.正常.ToString()
                                select p;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdatePMICounter(DcPMICounter model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPMICounter, PMICounter>());
                    var product = Mapper.Map<PMICounter>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}