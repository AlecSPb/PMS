using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;
using System.Data.Entity;

namespace PMSWCFService
{
    public class EFModel
    {
        public DeliveryItem Delivery { get; set; }
        public RecordBonding Bond { get; set; }
        public RecordTest Test { get; set; }
    }


    public class OutputService : IOutputService
    {
        [Obsolete]
        public List<PMS230DataModel> GetAll230Data(int s, int t)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from dd in db.DeliveryItems
                                join tt in db.RecordTests on dd.ProductID equals tt.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on dd.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where dd.State == PMSCommon.SimpleState.正常.ToString()
                                && bbdata.State == PMSCommon.CommonState.已核验.ToString()
                                && ccdata.State == PMSCommon.BondingState.最终完成.ToString()
                                && (dd.Customer == "Midsummer" || dd.Customer == "Chaozhou")
                                && dd.Dimension.Contains("230")
                                orderby dd.ProductID descending
                                select new EFModel
                                {
                                    Delivery = dd,
                                    Test = bbdata,
                                    Bond = ccdata
                                };

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<EFModel, PMS230DataModel>();
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                        cfg.CreateMap<RecordTest, DcRecordTest>();
                        cfg.CreateMap<RecordBonding, DcRecordBonding>();
                    });

                    var new_models = query.Skip(s).Take(t).ToList();

                    var models = Mapper.Map<List<EFModel>, List<PMS230DataModel>>(new_models);
                    return models;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcOutputSpecialFor230Model> GetAll230DataByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endtime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var query = from dd in db.DeliveryItems
                                join tt in db.RecordTests on dd.ProductID equals tt.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on dd.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where dd.State == PMSCommon.SimpleState.正常.ToString()
                                && bbdata.State == PMSCommon.CommonState.已核验.ToString()
                                && ccdata.State == PMSCommon.BondingState.最终完成.ToString()
                                && (dd.Customer == "Midsummer" || dd.Customer == "Chaozhou")
                                && dd.Dimension.Contains("230")
                                && dd.CreateTime >= startTime
                                && dd.CreateTime <= endtime
                                orderby dd.ProductID descending
                                select new DcOutputSpecialFor230Model
                                {
                                    ProductID = dd.ProductID,
                                    Customer = dd.Customer,
                                    Composition = dd.Composition,
                                    CompositionAbbr = bbdata.CompositionAbbr,
                                    Dimension = dd.Dimension,
                                    DimensionActual = dd.DimensionActual,
                                    Weight = dd.Weight,
                                    Density = bbdata.Density,
                                    Resistance = bbdata.Resistance,
                                    PlateLot = ccdata.PlateLot,
                                    PlateType = ccdata.PlateType,
                                    BondCreateTime = ccdata.CreateTime,
                                    WeldingRate = ccdata.WeldingRate,
                                    DeliveryCreateTime = dd.CreateTime,
                                    Position = dd.Position,
                                    CompositionXRF = bbdata.CompositionXRF
                                };
                    var new_models = query.Skip(s).Take(t).ToList();

                    //var models = Mapper.Map<List<EFModel>, List<PMS230DataModel>>(new_models);
                    return new_models;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetAll230DataByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var query = from dd in db.DeliveryItems
                                join t in db.RecordTests on dd.ProductID equals t.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on dd.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where dd.State == PMSCommon.SimpleState.正常.ToString()
                                && bbdata.State == PMSCommon.CommonState.已核验.ToString()
                                && ccdata.State == PMSCommon.BondingState.最终完成.ToString()
                                && (dd.Customer == "Midsummer" || dd.Customer == "Chaozhou")
                                && dd.Dimension.Contains("230")
                                && dd.CreateTime >= startTime
                                && dd.CreateTime <= endTime
                                select dd;


                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        [Obsolete]
        public int GetAll230DataCount()
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from d in db.DeliveryItems
                                join t in db.RecordTests on d.ProductID equals t.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on d.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where d.State == PMSCommon.SimpleState.正常.ToString()
                                && bbdata.State == PMSCommon.CommonState.已核验.ToString()
                                && ccdata.State == PMSCommon.BondingState.最终完成.ToString()
                                && (d.Customer == "Midsummer" || d.Customer == "Chaozhou")
                                && d.Dimension.Contains("230")
                                select new
                                {
                                    Delivery = d,
                                    Test = bbdata,
                                    Bond = ccdata
                                };


                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcConsumableInventory> GetConsumableInventoryByYearMonth(int s, int t)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumableInventories
                                where p.State != PMSCommon.SimpleState.作废.ToString()
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

        public int GetConsumableInventoryByYearMonthCount()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.ConsumableInventories
                                where p.State != PMSCommon.SimpleState.作废.ToString()
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

        public List<DcConsumablePurchase> GetConsumablePurchaseByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.ConsumablePurchases
                                where p.State != PMSCommon.SimpleState.作废.ToString()
                                && p.CreateTime >= startTime && p.CreateTime <= endTime
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

        public int GetConsumablePurchaseByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.ConsumablePurchases
                                where p.State != PMSCommon.SimpleState.作废.ToString()
                                && p.CreateTime >= startTime && p.CreateTime <= endTime
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

        public List<DcDelivery> GetDeliveries(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.Deliverys
                                where m.CreateTime >= startTime
                                && m.CreateTime <= endTime
                                && m.State != PMSCommon.DeliveryState.作废.ToString()
                                orderby m.CreateTime descending
                                select m;
                    return mapper.Map<List<Delivery>, List<DcDelivery>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialOrderItemExtra> GetMaterialOrderItemsByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSMaterialOrderItemExtra, DcMaterialOrderItemExtra>();
                        cfg.CreateMap<MaterialOrder, DcMaterialOrder>();
                        cfg.CreateMap<MaterialOrderItem, DcMaterialOrderItem>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.CreateTime >= startTime
                                && m.CreateTime <= endTime
                                && m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
                                orderby m.CreateTime descending
                                select new PMSMaterialOrderItemExtra
                                {
                                    MaterialOrder = mm,
                                    MaterialOrderItem = m
                                };
                    return mapper.Map<List<PMSMaterialOrderItemExtra>, List<DcMaterialOrderItemExtra>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialOrderItemsByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var query = from m in dc.MaterialOrderItems
                                join mm in dc.MaterialOrders on m.MaterialOrderID equals mm.ID
                                where m.CreateTime >= startTime
                                && m.CreateTime <= endTime
                                && m.State != PMSCommon.MaterialOrderItemState.作废.ToString()
                                && mm.State != PMSCommon.MaterialOrderState.作废.ToString()
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

        public List<DcOrder> GetOrderByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                    });
                    var mapper = config.CreateMapper();
                    var query = from o in dc.Orders
                                where o.CreateTime >= startTime
                                && o.CreateTime <= endTime
                                && o.State != OrderState.作废.ToString()
                                orderby o.CreateTime descending
                                select o;

                    var result = mapper.Map<List<PMSOrder>, List<DcOrder>>(query.Skip(s).Take(t).ToList());
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetOrderByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    return dc.Orders.Where(o => o.CreateTime >= startTime
                    && o.CreateTime <= endTime
                    && o.State != OrderState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcPlanExtra> GetPlanByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && p.PlanDate >= startTime
                                && p.PlanDate <= endTime
                                orderby p.PlanDate descending, p.PlanLot descending
                                select new PMSPlanExtraModel() { Plan = p, Misson = o };

                    var final = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanExtraModel, DcPlanExtra>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanExtraModel>, List<DcPlanExtra>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetPlanByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && p.PlanDate >= startTime
                                && p.PlanDate <= endTime
                                select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanTrace> GetPlanTrace(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他" || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                && p.PlanDate >= startTime
                                && p.PlanDate <= endTime
                                orderby p.PlanDate descending, p.PlanLot descending, p.VHPDeviceCode descending, p.CreateTime descending
                                select new DcPlanTrace
                                {
                                    ID = p.ID,
                                    SearchCode = p.SearchCode,
                                    PlanDate = p.PlanDate,
                                    PlanType = p.PlanType,
                                    VHPDeviceCode = p.VHPDeviceCode,
                                    CompositionStd = o.CompositionStandard,
                                    MoldDiameter = p.MoldDiameter,
                                    Quantity = p.Quantity,
                                    Customer = o.CustomerName,
                                    PMINumber = o.PMINumber,
                                    Dimension = o.Dimension,
                                    OrderQuantity = o.Quantity
                                };

                    var result = query.Skip(s).Take(t).ToList();

                    //查询每个进度情况
                    foreach (var item in result)
                    {
                        item.RecordDeMold = AnlysisHelper.CheckRecordDemold(item.SearchCode);
                        item.RecordMachine = AnlysisHelper.CheckRecordMachine(item.SearchCode);
                        item.RecordTest = AnlysisHelper.CheckRecordTest(item.SearchCode);
                        item.RecordBonding = AnlysisHelper.CheckRecordBonding(item.SearchCode);
                        item.RecordDelivery = AnlysisHelper.CheckDelivery(item.SearchCode);
                        item.RecordFailure = AnlysisHelper.CheckFailure(item.SearchCode);
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

        public int GetPlanTraceCount(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.VHPPlans
                                join o in dc.Orders on p.OrderID equals o.ID
                                where p.State == PMSCommon.CommonState.已核验.ToString()
                                && (p.PlanType == "加工" || p.PlanType == "其他" || p.PlanType == "外协" || p.PlanType == "代工" || p.PlanType == "发货")
                                && p.PlanDate >= startTime
                                && p.PlanDate <= endTime
                                select new DcPlanTrace
                                {
                                    ID = p.ID,
                                };
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordBonding> GetRecordBondingByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.RecordBondings
                                where p.CreateTime >= startTime
                                && p.CreateTime <= endTime
                                && p.State != BondingState.作废.ToString()
                                orderby p.TargetProductID descending
                                select p;
                    var result = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    var products = Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetRecordBondingCountByYearMonth(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.RecordBondings
                                where p.CreateTime >= startTime
                                && p.CreateTime <= endTime
                                && p.State != BondingState.作废.ToString()
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

        public List<DcRecordTest> GetRecordTestByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.RecordTests
                                where p.CreateTime >= startTime
                                && p.CreateTime <= endTime
                                && p.State != CommonState.作废.ToString()
                                orderby p.ProductID descending
                                select p;
                    var result = query.Skip(s).Take(t).ToList();
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(result);
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetRecordTestCountByYearMonth(int year_start, int month_start, int year_end, int month_end)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    DateTime startTime = new DateTime(year_start, month_start, 1, 0, 0, 0);
                    DateTime endTime = new DateTime(year_end, month_end, 1, 23, 59, 59).AddMonths(1).AddDays(-1);
                    var query = from p in dc.RecordTests
                                where p.CreateTime >= startTime
                                && p.CreateTime <= endTime
                                && p.State != CommonState.作废.ToString()
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
    }
}
