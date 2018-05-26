using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using AutoMapper;


namespace PMSWCFService.ServiceImplements
{
    public partial class ExtraService : IToolInventoryService
    {
        public IList<DcToolFilling> GetToolFillings(string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    var query = from t in service.ToolFillings
                                where t.CompositionAbbr.Contains(elementA)
                                && t.CompositionAbbr.Contains(elementB)
                                orderby t.ToolNumber, t.CompositionAbbr
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolFilling, DcToolFilling>());
                    return Mapper.Map<List<ToolFilling>, List<DcToolFilling>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetToolFillingsCount()
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public IList<DcToolMilling> GetToolMillings(string elementA, string elementB)
        {
            try
            {
                using (var service = new PMSDbContext())
                {
                    var query = from t in service.ToolMillings
                                where t.CompositionAbbr.Contains(elementA)
                                && t.CompositionAbbr.Contains(elementB)
                                orderby t.ToolNumber, t.CompositionAbbr
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<ToolMilling, DcToolMilling>());
                    return Mapper.Map<List<ToolMilling>, List<DcToolMilling>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetToolMillingsCount()
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }
    }
}