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
    public class ConsumableService : IConsumableService
    {
        public void AddConsumableInventory(DcConsumableInventory model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcConsumableInventory, ConsumableInventory>());
                    var entity = Mapper.Map<ConsumableInventory>(model);
                    dc.ConsumableInventories.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void AddConsumablePurchase(DcConsumablePurchase model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcConsumablePurchase, ConsumablePurchase>());
                    var entity = Mapper.Map<ConsumablePurchase>(model);
                    dc.ConsumablePurchases.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcConsumableInventory> GetConsumableInventory(int s, int t, string itemname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumableInventories
                                where p.ItemName.Contains(itemname)
                                && p.State != PMSCommon.SimpleState.作废.ToString()
                                orderby p.Category, p.ItemName
                                select p;
                    Mapper.Initialize(cfg => cfg.CreateMap<ConsumableInventory, DcConsumableInventory>());
                    var models = Mapper.Map<List<ConsumableInventory>, List<DcConsumableInventory>>(query.Skip(s).Take(t).ToList());
                    return models;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetConsumableInventoryCount(string item)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumableInventories
                                where p.ItemName.Contains(item)
                                && p.State != PMSCommon.SimpleState.作废.ToString()
                                select p;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcConsumableInventory> GetConsumableInventoryWarning()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumableInventories
                                where (p.Quantity < p.MinWarningQuantity || p.Quantity > p.MaxWarningQuantity)
                                && p.State != PMSCommon.SimpleState.作废.ToString()
                                orderby p.Category, p.ItemName
                                select p;
                    Mapper.Initialize(cfg => cfg.CreateMap<ConsumableInventory, DcConsumableInventory>());
                    var models = Mapper.Map<List<ConsumableInventory>, List<DcConsumableInventory>>(query.ToList());
                    return models;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcConsumablePurchase> GetConsumablePurchase(int s, int t, string itemname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumablePurchases
                                where p.ItemName.Contains(itemname)
                                && p.State != PMSCommon.SimpleState.作废.ToString()
                                orderby p.CreateTime descending
                                select p;
                    Mapper.Initialize(cfg => cfg.CreateMap<ConsumablePurchase, DcConsumablePurchase>());
                    var models = Mapper.Map<List<ConsumablePurchase>, List<DcConsumablePurchase>>(query.Skip(s).Take(t).ToList());
                    return models;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetConsumablePurchaseCount(string item)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumablePurchases
                                where p.ItemName.Contains(item)
                                && p.State != PMSCommon.SimpleState.作废.ToString()
                                select p;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateConsumableInventory(DcConsumableInventory model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcConsumableInventory, ConsumableInventory>());
                    var product = Mapper.Map<ConsumableInventory>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateConsumablePurchase(DcConsumablePurchase model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcConsumablePurchase, ConsumablePurchase>());
                    var product = Mapper.Map<ConsumablePurchase>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
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