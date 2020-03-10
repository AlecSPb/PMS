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
        public void AddToolSieve(DcToolSieve model)
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcToolSieve> GetToolSieve(string searchid, string materialGroup, int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolSieve, DcToolSieve>());
                    var query = from i in dc.ToolSieves
                                where (i.State == PMSCommon.ToolState.正常.ToString()
                                ||i.State==PMSCommon.ToolState.停止使用.ToString())
                                && i.SearchID.Contains(searchid)
                                && i.MaterialGroup.Contains(materialGroup)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ToolSieve>, List<DcToolSieve>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetToolSieveCount(string searchid, string materialGroup)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ToolSieves
                                where (i.State == PMSCommon.ToolState.正常.ToString()
                                || i.State == PMSCommon.ToolState.停止使用.ToString())
                                && i.SearchID.Contains(searchid)
                                && i.MaterialGroup.Contains(materialGroup)
                                orderby i.CreateTime descending
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcToolSieve> GetToolSieveUsed(string searchid, string materialGroup, int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolSieve, DcToolSieve>());
                    var query = from i in dc.ToolSieves
                                where i.State == PMSCommon.ToolState.正常.ToString()
                                && i.SearchID.Contains(searchid)
                                && i.MaterialGroup.Contains(materialGroup)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ToolSieve>, List<DcToolSieve>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetToolSieveUsedCount(string searchid, string materialGroup)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ToolSieves
                                where i.State == PMSCommon.ToolState.正常.ToString()
                                && i.SearchID.Contains(searchid)
                                && i.MaterialGroup.Contains(materialGroup)
                                orderby i.CreateTime descending
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateToolSieve(DcToolSieve model)
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
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}