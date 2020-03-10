using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using AutoMapper;


namespace PMSWCFService
{
    public partial class ExtraService : IToolInventoryService
    {
        public int AddToolFilling(DcToolFilling model)
        {
            try
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcToolFilling, ToolFilling>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<ToolFilling>(model);
                    db.ToolFillings.Add(entity);
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddToolMilling(DcToolMilling model)
        {
            try
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcToolMilling, ToolMilling>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<ToolMilling>(model);
                    db.ToolMillings.Add(entity);
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteToolFilling(Guid id)
        {
            try
            {
                int result = 0;
                using (var db = new PMSDbContext())
                {
                    var entity = db.ToolFillings.Find(id);
                    db.ToolFillings.Remove(entity);
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteToolMilling(Guid id)
        {
            try
            {
                int result = 0;
                using (var db = new PMSDbContext())
                {
                    var entity = db.ToolMillings.Find(id);
                    db.ToolMillings.Remove(entity);
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }


        public IList<DcToolFilling> GetToolFillings(int s, int t, string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    var query = from tt in service.ToolFillings
                                where tt.CompositionAbbr.Contains(elementA)
                                && tt.CompositionAbbr.Contains(elementB)
                                && tt.State != PMSCommon.SimpleState.作废.ToString()
                                orderby tt.ToolNumber descending, tt.CompositionAbbr
                                select tt;
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolFilling, DcToolFilling>());
                    return Mapper.Map<List<ToolFilling>, List<DcToolFilling>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetToolFillingsCount(string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    return service.ToolFillings.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public IList<DcToolMilling> GetToolMillings(int s, int t, string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    var query = from tt in service.ToolMillings
                                where tt.CompositionAbbr.Contains(elementA)
                                && tt.CompositionAbbr.Contains(elementB)
                                && tt.State != PMSCommon.SimpleState.作废.ToString()
                                orderby tt.ToolNumber descending, tt.CompositionAbbr
                                select tt;
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolMilling, DcToolMilling>());
                    return Mapper.Map<List<ToolMilling>, List<DcToolMilling>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetToolMillingsCount(string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    return service.ToolMillings.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateToolFilling(DcToolFilling model)
        {
            try
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcToolFilling, ToolFilling>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<ToolFilling>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateToolMilling(DcToolMilling model)
        {
            try
            {
                int result = 0;
                Mapper.Initialize(cfg => cfg.CreateMap<DcToolMilling, ToolMilling>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<ToolMilling>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}