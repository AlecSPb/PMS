using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSCommon;
using System.Data.Entity;

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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialNeedByUID(DcMaterialNeed model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddMaterialNeed(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialOrderByUID(DcMaterialOrder model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddMaterialOrder(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialOrderItemByUID(DcMaterialOrderItem model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddMaterialOrderItem(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                        model.State = MaterialOrderState.作废.ToString();
                        //同时作废改订单下面的所有项目
                        var items = dc.MaterialOrderItems.Where(i => i.MaterialOrderID == id);
                        foreach (var item in items)
                        {
                            item.State = PMSCommon.MaterialOrderItemState.作废.ToString();
                        }

                        result = dc.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                        model.State = MaterialOrderItemState.作废.ToString();
                        result = dc.SaveChanges();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }


        public List<DcMaterialNeed> GetMaterialNeedBySearchInPage(int skip, int take, string composition, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<MaterialNeed, DcMaterialNeed>());
                    var mapper = config.CreateMapper();
                    var result = dc.MaterialNeeds.Where(m => m.Composition.Contains(composition)
                           && m.PMINumber.Contains(pminumber)
                        && m.State != SimpleState.作废.ToString())
                        .OrderByDescending(m => m.CreateTime)
                        .Skip(skip).Take(take)
                        .ToList();
                    return mapper.Map<List<MaterialNeed>, List<DcMaterialNeed>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialNeedCountBySearch(string composition, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialNeeds.Where(m => m.Composition.Contains(composition)
                    && m.PMINumber.Contains(pminumber)
                    && m.State != SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.State != MaterialOrderState.作废.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(supplier)
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrder>, List<DcMaterialOrder>>(query.Skip(skip).Take(take).ToList());

                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrder> GetMaterialOrderBySearch(int skip, int take, string orderPo, string supplier)
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
                                where m.State != MaterialOrderState.作废.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(supplier)
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrder>, List<DcMaterialOrder>>(query.Skip(skip).Take(take).ToList());

                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderCountBySearch(string orderPo, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrders
                                where m.State != MaterialOrderState.作废.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(supplier)
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.OrderPO.Contains(orderPo) && m.State == MaterialOrderState.已核验.ToString()
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.OrderPO.Contains(orderPo) && m.SupplierAbbr.Contains("SJ") && m.State == MaterialOrderState.已核验.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrder>, List<DcMaterialOrder>>(query.Skip(skip).Take(take).ToList());

                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                    var query = from m in dc.MaterialOrderItems
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString() && m.MaterialOrderID == id
                                orderby m.CreateTime descending
                                select m;
                    var result = query.ToList();
                    return mapper.Map<List<MaterialOrderItem>, List<DcMaterialOrderItem>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString() && m.MaterialOrderID == id
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtraByYear(int skip, int take, int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSMaterialOrderItemExtra, DcMaterialOrderItemExtra>();
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                        cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                && DbFunctions.DiffYears(m.CreateTime, date) == 0
                                orderby m.CreateTime descending
                                select new PMSMaterialOrderItemExtra
                                {
                                    MaterialOrder = mm,
                                    MaterialOrderItem = m
                                };
                    return mapper.Map<List<PMSMaterialOrderItemExtra>, List<DcMaterialOrderItemExtra>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemExtraCountByYear(string composition, string pminumber, int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                 && DbFunctions.DiffYears(m.CreateTime, date) == 0
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtras(int skip, int take, string composition,
            string pminumber, string orderitemnumber, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSMaterialOrderItemExtra, DcMaterialOrderItemExtra>();
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                        cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                && mm.Supplier.Contains(supplier)
                                orderby m.CreateTime descending
                                select new PMSMaterialOrderItemExtra
                                {
                                    MaterialOrder = mm,
                                    MaterialOrderItem = m
                                };
                    return mapper.Map<List<PMSMaterialOrderItemExtra>, List<DcMaterialOrderItemExtra>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemExtrasCount(string composition, string pminumber, string orderitemnumber, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                && mm.Supplier.Contains(supplier)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtrasUnCompleted(int skip, int take, string composition,
    string pminumber, string orderitemnumber, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSMaterialOrderItemExtra, DcMaterialOrderItemExtra>();
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                        cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State == PMSCommon.MaterialOrderItemState.未完成.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                && mm.Supplier.Contains(supplier)
                                orderby m.CreateTime
                                select new PMSMaterialOrderItemExtra
                                {
                                    MaterialOrder = mm,
                                    MaterialOrderItem = m
                                };
                    return mapper.Map<List<PMSMaterialOrderItemExtra>, List<DcMaterialOrderItemExtra>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemExtrasCountUnCompleted(string composition, string pminumber, string orderitemnumber, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State == PMSCommon.MaterialOrderItemState.未完成.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                && mm.Supplier.Contains(supplier)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<MaterialOrderItem>, List<DcMaterialOrderItem>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                    XS.Current.Error(ex);
                    throw ex;
                }
            }

        }

        public int UpdateMaterialNeedByUID(DcMaterialNeed model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateMaterialNeed(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialOrderByUID(DcMaterialOrder model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateMaterialOrder(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialOrderItemByUID(DcMaterialOrderItem model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateMaterialOrderItem(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }


        private void SaveHistory(DcMaterialNeed model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialNeed, MaterialNeedHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialNeedHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialNeedHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        private void SaveHistory(DcMaterialOrder model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialOrder, MaterialOrderHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialOrderHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialOrderHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        private void SaveHistory(DcMaterialOrderItem model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialOrderItem, MaterialOrderItemHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialOrderItemHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialOrderItemHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        //2017-8-31补充API
        public List<DcPlanHistory> GetPlanHistoryTop10(string searchCode, string deviceCode)
        {
            //TODO:获取计划历史纪录
            throw new NotImplementedException();
        }

        public bool CheckMaterialOrderUnChecked()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialOrders.Where(i => i.State == MaterialOrderState.未核验.ToString()).Count() > 0;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return false;
            }
        }









    }
}