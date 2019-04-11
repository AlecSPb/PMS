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
    public partial class ExtraService : IRemainInventoryService
    {
        public int AddRemainInventory(DcRemainInventory model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRemainInventory, RemainInventory>());
                    var entity = Mapper.Map<RemainInventory>(model);
                    dc.RemainInventories.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRemainInventory> GetRemainInventories(string productid, string composition, int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RemainInventories
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State != PMSCommon.InventoryState.作废.ToString()
                                orderby p.ProductID descending
                                select p;
                    var result = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RemainInventory, DcRemainInventory>());
                    var models = Mapper.Map<List<RemainInventory>, List<DcRemainInventory>>(result);
                    return models;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetRemainInventoryCounter(string productid, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.RemainInventories
                                where p.ProductID.Contains(productid)
                                && p.Composition.Contains(composition)
                                && p.State != PMSCommon.InventoryState.作废.ToString()
                                orderby p.ProductID descending
                                select p;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRemainInventory(DcRemainInventory model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRemainInventory, RemainInventory>());
                    var product = Mapper.Map<RemainInventory>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
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