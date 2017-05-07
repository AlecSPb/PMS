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
        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creaditor)
        {
            throw new NotImplementedException();
        }

        public int GetItemDebitCount(string itemType, string itemName, string creaditor)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialInventoryInCount(string composition, string batchnumber, string pminumber)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take, string composition, string batchnumber, string pminumber)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialInventoryOutCount(string composition, string batchnumber, string pminumber)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take, string composition, string batchnumber, string pminumber)
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
                                && m.Supplier.Contains("三杰")
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
                                && m.Supplier.Contains("三杰")
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

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtras(int skip, int take, string composition, string pminumber, string orderitemnumber)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialOrderItemExtrasCount(string composition, string pminumber, string orderitemnumber)
        {
            throw new NotImplementedException();
        }
    }
}