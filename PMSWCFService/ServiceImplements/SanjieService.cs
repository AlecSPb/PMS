using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;
using System.Data.Entity;

namespace PMSWCFService
{
    public class SanjieService : ISanjieService
    {
        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ItemDebit, DcItemDebit>());
                    var query = from i in dc.ItemDebits
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                && i.ItemName.Contains(itemName)
                                && i.Creditor.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ItemDebit>, List<DcItemDebit>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetItemDebitCount(string itemType, string itemName)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ItemDebits
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                && i.ItemName.Contains(itemName)
                                && i.Creditor.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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

        public List<DcMaterialInventoryIn> GetMaterialInventoryInByYear(int skip, int take, int year)
        {
            try
            {
                XS.RunLog();
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                 && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        public int GetMaterialInventoryInCountByYear(int year)
        {
            try
            {
                XS.RunLog();
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        public int GetMaterialInventoryInCount(string composition, string batchnumber, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }



        public List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take, string composition, string batchnumber, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCount(string composition, string batchnumber, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take, string composition, string batchnumber, string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCountByYear(int year)
        {
            try
            {
                XS.RunLog();
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOutsByYear(int skip, int take, int year)
        {
            try
            {
                XS.RunLog();
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrder> GetMaterialOrder(int skip, int take, string orderPo)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrders
                                where m.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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

        public int GetMaterialOrderCount(string orderPo)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrders
                                where m.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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

        public List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id)
        {
            try
            {
                XS.RunLog();
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

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtraByYear(int skip, int take, int year)
        {
            try
            {
                XS.RunLog();
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
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && DbFunctions.DiffYears(m.CreateTime, date) == 0
                                && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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
                XS.RunLog();
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                 && DbFunctions.DiffYears(m.CreateTime, date) == 0
                                 && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtras(int skip, int take, string composition, string pminumber, string orderitemnumber)
        {
            try
            {
                XS.RunLog();
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
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
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

        public int GetMaterialOrderItemExtrasCount(string composition, string pminumber, string orderitemnumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
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
        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtrasUnCompleted(int skip, int take, string composition, string pminumber, string orderitemnumber)
        {
            try
            {
                XS.RunLog();
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
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                orderby m.Priority, m.CreateTime
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

        public int GetMaterialOrderItemExtrasUnCompletedCount(string composition, string pminumber, string orderitemnumber)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State == PMSCommon.MaterialOrderItemState.未完成.ToString()
                                && mm.State == PMSCommon.MaterialOrderState.已核验.ToString()
                                && mm.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                && m.Composition.Contains(composition)
                                && m.PMINumber.Contains(pminumber)
                                && m.OrderItemNumber.Contains(orderitemnumber)
                                orderby m.CreateTime
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

        public int FinishMaterialOrder(Guid id, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var item = dc.MaterialOrders.Find(id);
                    item.State = PMSCommon.MaterialOrderState.已核验.ToString();
                    dc.Entry(item).State = EntityState.Modified;
                    SaveHistory(item, uid);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int FinishMaterialOrderItem(Guid id, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var item = dc.MaterialOrderItems.Find(id);
                    #region 存储入库数据
                    var materialIn = new DcMaterialInventoryIn();
                    materialIn.Id = Guid.NewGuid();
                    materialIn.Creator = uid;
                    materialIn.CreateTime = DateTime.Now;
                    materialIn.State = PMSCommon.InventoryState.暂入库.ToString();
                    materialIn.Supplier = PMSCommon.MaterialSupplier.三杰.ToString();
                    materialIn.MaterialLot = item.OrderItemNumber;
                    materialIn.PMINumber = item.PMINumber;
                    materialIn.Composition = item.Composition;
                    materialIn.Weight = item.Weight;
                    materialIn.Purity = item.Purity;
                    materialIn.Remark = "";

                    AddMaterialInventoryIn(materialIn);

                    #endregion
                    item.State = PMSCommon.MaterialOrderItemState.完成.ToString();
                    dc.Entry(item).State = EntityState.Modified;
                    SaveHistory(item, uid);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialOrderItem(DcMaterialOrderItem model, string uid)
        {
            try
            {
                XS.RunLog();
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialOrderItem, MaterialOrderItem>());
                    var item = Mapper.Map<MaterialOrderItem>(model);
                    dc.Entry(item).State = EntityState.Modified;
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }


        public int AddToMaterialIn(DcMaterialInventoryIn model, string uid)
        {
            try
            {
                XS.RunLog();
                SaveHistory(model, uid);
                return AddMaterialInventoryIn(model);
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int AddToCompound(DcBDCompound model, string uid)
        {
            try
            {
                XS.RunLog();
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    //如果熔点信息为空，也不写入
                    if (string.IsNullOrEmpty(model.MeltingPoint.Trim()))
                        return result;
                    //判断当前成分是否存在，不存在再写入
                    var count = dc.Compounds.Where(i => i.MaterialName == model.MaterialName).Count();
                    if (count == 0)
                    {
                        Mapper.Initialize(cfg => cfg.CreateMap<DcBDCompound, BDCompound>());
                        var item = Mapper.Map<BDCompound>(model);
                        dc.Compounds.Add(item);
                        result = dc.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 添加材料到暂入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private int AddMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            try
            {
                XS.RunLog();
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryIn, MaterialInventoryIn>());
                    var item = Mapper.Map<MaterialInventoryIn>(model);
                    dc.MaterialInventoryIns.Add(item);
                    result = dc.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }


        private void SaveHistory(DcMaterialInventoryIn model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialInventoryIn, MaterialInventoryInHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialInventoryInHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialInventoryInHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(MaterialOrder model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MaterialOrder, MaterialOrderHistory>();
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
        private void SaveHistory(MaterialOrderItem model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MaterialOrderItem, MaterialOrderItemHistory>();
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

        public List<DcMaterialInventoryIn> GetMaterialInventoryInTemporary()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State == PMSCommon.InventoryState.暂入库.ToString()
                                && o.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.ToList());
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