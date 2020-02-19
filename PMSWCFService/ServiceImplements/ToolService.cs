using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public class ToolService : IToolSieveService
    {
        public void AddToolSieve(ToolSieve model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcToolSieve, ToolSieve>());
                    var entity = Mapper.Map<ToolSieve>(model);
                    dc.ToolSieves.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcToolSieve> GetToolSieve()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolSieve, DcToolSieve>());
                    var query = from i in dc.ToolSieves
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ToolSieve>, List<DcToolSieve>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetToolSieveCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ToolSieves
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                orderby i.CreateTime descending
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public void UpdateToolSieve(ToolSieve model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcToolSieve, ToolSieve>());
                    var entity = Mapper.Map<ToolSieve>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}