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
                    DateTime startTime = new DateTime(year_start, month_start, 1);
                    DateTime endtime = new DateTime(year_end, month_end, 1).AddMonths(1).AddDays(-1);

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
                                    ProductID=dd.ProductID,
                                    Customer=dd.Customer,
                                    Composition=dd.Composition,
                                    CompositionAbbr=bbdata.CompositionAbbr,
                                    Dimension=dd.Dimension,
                                    DimensionActual=dd.DimensionActual,
                                    Weight=dd.Weight,
                                    Density=bbdata.Density,
                                    Resistance=bbdata.Resistance,
                                    PlateLot=ccdata.PlateLot,
                                    PlateType=ccdata.PlateType,
                                    BondCreateTime=ccdata.CreateTime,
                                    WeldingRate=ccdata.WeldingRate,
                                    DeliveryCreateTime=dd.CreateTime,
                                    Position=dd.Position,
                                    CompositionXRF=bbdata.CompositionXRF
                                };

                    //Mapper.Initialize(cfg =>
                    //{
                    //    cfg.CreateMap<EFModel, PMS230DataModel>();
                    //    cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                    //    cfg.CreateMap<RecordTest, DcRecordTest>();
                    //    cfg.CreateMap<RecordBonding, DcRecordBonding>();
                    //});

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
                    DateTime startTime = new DateTime(year_start, month_start, 1);
                    DateTime endtime = new DateTime(year_end, month_end, 1).AddMonths(1).AddDays(-1);

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
                                && dd.CreateTime <= endtime
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

        public List<DcOrder> GetOrderByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            throw new NotImplementedException();
            XS.RunLog();
            //try
            //{
            //    using (var dc = new PMSDbContext())
            //    {
            //        var config = new MapperConfiguration(cfg =>
            //        {
            //            cfg.CreateMap<PMSOrder, DcOrder>();
            //        });
            //        var mapper = config.CreateMapper();
            //        var query = from o in dc.Orders
            //                    where o.CustomerName.Contains(customer)
            //                    && o.CompositionStandard.Contains(compositionstd)
            //                    && o.State != OrderState.作废.ToString()
            //                    orderby o.CreateTime descending
            //                    select o;

            //        var result = mapper.Map<List<PMSOrder>, List<DcOrder>>(query.Skip(skip).Take(take).ToList());
            //        return result;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XS.Current.Error(ex);
            //    throw ex;
            //}
        }

        public int GetOrderByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            XS.RunLog();
            //try
            //{
            //    using (var dc = new PMSDbContext())
            //    {
            //        return dc.Orders.Where(o => o.CustomerName.Contains(customer)
            //        && o.CompositionStandard.Contains(compositionstd)
            //        && o.State != OrderState.作废.ToString()).Count();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XS.Current.Error(ex);
            //    throw ex;
            //}
            throw new NotImplementedException();

        }

        public List<DcPlanExtra> GetPlanByYearMonth(int s, int t, int year_start, int month_start, int year_end, int month_end)
        {
            //try
            //{
            //    XS.RunLog();
            //    using (var dc = new PMSDAL.PMSDbContext())
            //    {
            //        var query = from p in dc.VHPPlans
            //                    join o in dc.Orders on p.OrderID equals o.ID
            //                    where p.State == PMSCommon.CommonState.已核验.ToString()
            //                         && p.SearchCode.Contains(searchCode)
            //                         && o.CompositionStandard.Contains(composition)
            //                         && o.PMINumber.Contains(pminumber)
            //                    orderby DbFunctions.TruncateTime(p.PlanDate) descending, p.PlanLot descending, p.VHPDeviceCode descending, DbFunctions.TruncateTime(p.CreateTime) descending
            //                    select new PMSPlanWithMissonModel() { Plan = p, Misson = o };

            //        var final = query.Skip(skip).Take(take).ToList();
            //        Mapper.Initialize(cfg =>
            //        {
            //            cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
            //            cfg.CreateMap<PMSOrder, DcOrder>();
            //            cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
            //        });

            //        var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

            //        return result;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XS.Current.Error(ex);
            //    throw ex;
            //}
            throw new NotImplementedException();
        }

        public int GetPlanByYearMonthCount(int year_start, int month_start, int year_end, int month_end)
        {
            //try
            //{
            //    XS.RunLog();
            //    using (var dc = new PMSDAL.PMSDbContext())
            //    {
            //        var query = from p in dc.VHPPlans
            //                    join o in dc.Orders on p.OrderID equals o.ID
            //                    where p.State == PMSCommon.CommonState.已核验.ToString()
            //                         && p.SearchCode.Contains(searchCode)
            //                         && o.CompositionStandard.Contains(composition)
            //                         && o.PMINumber.Contains(pminumber)
            //                    select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
            //        return query.Count();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XS.Current.Error(ex);
            //    throw ex;
            //}
            throw new NotImplementedException();
        }
    }
}