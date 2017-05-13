using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public class SanjieService : ISanjieService
    {
        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetItemDebitCount(string itemType, string itemName)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryInByYear(int skip, int take, int year)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialInventoryInCount(string composition, string batchnumber, string pminumber)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryInCountByYear(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take, string composition, string batchnumber, string pminumber)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCount(string composition, string batchnumber, string pminumber)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCountByYear(int year)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take, string composition, string batchnumber, string pminumber)
        {
            try
            {
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOutsByYear(int skip, int take, int year)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialOrder> GetMaterialOrder(int skip, int take, string orderPo)
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
                                && m.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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

        public int GetMaterialOrderCount(string orderPo)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrders
                                where m.State != OrderState.作废.ToString()
                                && m.OrderPO.Contains(orderPo)
                                && m.Supplier.Contains(PMSCommon.MaterialSupplier.三杰.ToString())
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtraByYear(int skip, int take, int year)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialOrderItemExtraCountByYear(string composition, string pminumber, int year)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtras(int skip, int take, string composition, string pminumber, string orderitemnumber)
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemExtrasCount(string composition, string pminumber, string orderitemnumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
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
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}