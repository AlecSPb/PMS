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
            throw new NotImplementedException();
        }

        public int AddToolMilling(DcToolMilling model)
        {
            throw new NotImplementedException();
        }

        public int DeleteToolFilling(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteToolMilling(Guid id)
        {
            throw new NotImplementedException();
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
                                orderby tt.ToolNumber, tt.CompositionAbbr
                                select tt;
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
                LocalService.CurrentLog.Error(ex);
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
                                orderby tt.ToolNumber, tt.CompositionAbbr
                                select tt;
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateToolFilling(DcToolFilling model)
        {
            throw new NotImplementedException();
        }

        public int UpdateToolMilling(DcToolMilling model)
        {
            throw new NotImplementedException();
        }
    }
}