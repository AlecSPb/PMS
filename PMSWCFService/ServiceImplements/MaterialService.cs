using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IMaterialNeedService,
        IMaterialOrderService
    {
        public int AddMaterialNeed(DcMaterialNeed model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialNeed, MaterialNeed>());
                    var mapper = config.CreateMapper();
                    var materialNeed = mapper.Map<MaterialNeed>(model);
                    dc.MaterialNeeds.Add(materialNeed);
                    result = dc.SaveChanges();

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialOrder(DcMaterialOrder model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialOrder, MaterialOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var materialOrder = mapper.Map<MaterialOrder>(model);
                    dc.MaterialOrders.Add(materialOrder);
                    result = dc.SaveChanges();

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        public int AddMaterialOrderItem(DcMaterialOrderItem model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrderItem, MaterialOrderItem>());
                    var mapper = config.CreateMapper();
                    var materialOrderItem = mapper.Map<MaterialOrderItem>(model);
                    dc.MaterialOrderItems.Add(materialOrderItem);
                    result = dc.SaveChanges();

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public bool CheckOrderItemNumberExist(string orderItemnumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialOrderItems
                                where o.OrderItemNumber == orderItemnumber
                                select o;
                    return query.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteMaterialNeed(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var model = dc.MaterialNeeds.Find(id);
                    if (model != null)
                    {
                        model.State = SimpleState.作废.ToString();
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteMaterialOrder(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var model = dc.MaterialOrders.Find(id);
                    if (model != null)
                    {
                        model.State = OrderState.作废.ToString();
                        result = dc.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteMaterialOrderItem(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var model = dc.MaterialOrderItems.Find(id);
                    if (model != null)
                    {
                        model.State = SimpleState.作废.ToString();
                        result = dc.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialNeed> GetMaterialNeedBySearchInPage(int skip, int take, string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<MaterialNeed, DcMaterialNeed>());
                    var mapper = config.CreateMapper();
                    var result = dc.MaterialNeeds.Where(m => m.Composition.Contains(composition)
                        && m.State != SimpleState.作废.ToString())
                        .OrderByDescending(m => m.CreateTime)
                        .Skip(skip).Take(take)
                        .ToList();
                    return mapper.Map<List<MaterialNeed>, List<DcMaterialNeed>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialNeedCountBySearch(string composition)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialNeeds.Where(m => m.Composition.Contains(composition)
                    && m.State != SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrder> GetMaterialOrderBySearchInPage(int skip, int take, string orderPo, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrders
                                where m.State != OrderState.作废.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(supplier)
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrder>, List<DcMaterialOrder>>(query.Skip(skip).Take(take).ToList());

                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderCountBySearch(string orderPo, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialOrders.Where(m => m.OrderPO.Contains(supplier) && m.State != OrderState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderCountForSanjie(string orderPo)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrders
                                where m.OrderPO.Contains(orderPo) &&
                                (m.State == OrderState.暂停.ToString()
                                || m.State == OrderState.完成.ToString()
                                || m.State == OrderState.未完成.ToString())
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrder> GetMaterialOrderForSanjie(int skip, int take, string orderPo)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrders
                                where m.OrderPO.Contains(orderPo) && m.SupplierAbbr.Contains("SJ") &&
                                (m.State == OrderState.暂停.ToString()
                                || m.State == OrderState.完成.ToString()
                                || m.State == OrderState.未完成.ToString())
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrder>, List<DcMaterialOrder>>(query.Skip(skip).Take(take).ToList());

                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>());
                    var mapper = config.CreateMapper();
                    var result = dc.MaterialOrderItems.Where(m => m.MaterialOrderID == id).OrderByDescending(m => m.CreateTime).ToList();
                    return mapper.Map<List<MaterialOrderItem>, List<DcMaterialOrderItem>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemCountByMaterialID(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                where m.State != PMSCommon.SimpleState.作废.ToString() && m.MaterialOrderID==id
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItem> GetMaterialOrderItems(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>());
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrderItems
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrderItem>, List<DcMaterialOrderItem>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemsCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialNeed(DcMaterialNeed model)
        {

            using (var dc = new PMSDbContext())
            {
                try
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialNeed, MaterialNeed>());
                    var mapper = config.CreateMapper();
                    var materialNeed = mapper.Map<MaterialNeed>(model);
                    dc.Entry(materialNeed).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    LocalService.CurrentLog.Error(ex);
                    throw ex;
                }
            }

        }

        public int UpdateMaterialOrder(DcMaterialOrder model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialOrder, MaterialOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var materialOrder = mapper.Map<MaterialOrder>(model);
                    dc.Entry(materialOrder).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialOrderItem(DcMaterialOrderItem model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrderItem, MaterialOrderItem>());
                    var mapper = config.CreateMapper();
                    var materialOrderItem = mapper.Map<MaterialOrderItem>(model);
                    dc.Entry(materialOrderItem).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return result;
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