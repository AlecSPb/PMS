using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IMissonService
    {
        public List<DcOrder> GetMissons(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType.Contains("VHP")
                                 && (o.State == OrderState.UnCompleted.ToString()
                                 || o.State == OrderState.Paused.ToString()
                                 || o.State == OrderState.Completed.ToString())
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType.Contains("VHP")
                                 && (o.State == OrderState.UnCompleted.ToString()
                                 || o.State == OrderState.Paused.ToString()
                                 || o.State == OrderState.Completed.ToString())
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

        public List<DcPlanVHP> GetPlans(Guid orderid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                    var result = from p in dc.VHPPlans
                                 where p.OrderID == orderid && p.State != OrderState.Deleted.ToString()
                                 orderby p.PlanDate descending
                                 select p;
                    var plans = Mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(result.ToList());
                    return plans;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMissonWithPlan> GetMissonWithPlan(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    //var query = (from p in dc.VHPPlans
                    //             where p.State != PMSCommon.CommonState.Deleted.ToString()
                    //             orderby p.PlanDate descending
                    //             select p).Skip(skip).Take(take);
                    //var query2 = from p in query
                    //             join o in dc.Orders
                    //             on p.OrderID equals o.ID
                    //             select new { Plan = p, Order = o };
                    //foreach (var item in query2)
                    //{
                    //    Console.WriteLine(item.Plan);
                    //}
                    #region 以后再改
                    var result = dc.VHPPlans.Where(p => p.State != PMSCommon.CommonState.Deleted.ToString())
                  .OrderByDescending(p => p.PlanDate)
                  .Skip(skip).Take(take)
                  .Join(dc.Orders, p => p.OrderID, o => o.ID, (p, o) => new DcMissonWithPlan()
                  {
                      OrderID = o.ID,
                      PlanID = p.ID,
                      CustomerName = o.CustomerName,
                      PO = o.PO,
                      PMINumber = o.PMINumber,
                      CompositionStandard = o.CompositionStandard,
                      CompositionAbbr = o.CompositionAbbr,
                      CompositionOriginal = o.CompositionOriginal,
                      ProductType = o.ProductType,
                      Purity = o.Purity,
                      Quantity = o.Quantity,
                      QuantityUnit = o.QuantityUnit,
                      Dimension = o.Dimension,
                      DimensionDetails = o.DimensionDetails,
                      SampleNeed = o.SampleNeed,
                      DeadLine = o.DeadLine,
                      MinimumAcceptDefect = o.MinimumAcceptDefect,
                      OrderRemark = o.Remark,
                      Creator = p.Creator,
                      CreateTime = p.CreateTime,
                      PlanDate = p.PlanDate,
                      PlanLot = p.PlanLot,
                      VHPDeviceCode = p.VHPDeviceCode,
                      MoldType = p.MoldType,
                      CalculationDensity = p.CalculationDensity,
                      MoldDiameter = p.MoldDiameter,
                      Thickness = p.Thickness,
                      TargetQuantity = p.Quantity,
                      SingleWeight = p.SingleWeight,
                      AllWeight = p.AllWeight,
                      GrainSize = p.GrainSize,
                      RoomHumidity = p.RoomHumidity,
                      RoomTemperature = p.RoomTemperature,
                      PreTemperature = p.PreTemperature,
                      PrePressure = p.PrePressure,
                      Temperature = p.Temperature,
                      Pressure = p.Pressure,
                      Vaccum = p.Vaccum,
                      KeepTempTime = p.KeepTempTime,
                      ProcessCode = p.ProcessCode,
                      MillingRequirement = p.MillingRequirement,
                      FillingRequirement = p.FillingRequirement,
                      VHPPlanRemark = p.Remark,
                      VHPRequirement = p.VHPRequirement,
                      MachineRequirement = p.MachineRequirement,
                      SpecialRequirement = p.SpecialRequirement
                  }).ToList();
                    #endregion
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetMissonWithPlanCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    return dc.VHPPlans.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public List<DcMissonWithPlan> GetMissonWithPlanChecked(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    #region 以后再改
                    var result = dc.VHPPlans.Where(p => p.State == PMSCommon.CommonState.Checked.ToString())
                  .OrderByDescending(p => p.PlanDate)
                  .Skip(skip).Take(take)
                  .Join(dc.Orders, p => p.OrderID, o => o.ID, (p, o) => new DcMissonWithPlan()
                  {
                      OrderID = o.ID,
                      PlanID = p.ID,
                      CustomerName = o.CustomerName,
                      PO = o.PO,
                      PMINumber = o.PMINumber,
                      CompositionStandard = o.CompositionStandard,
                      CompositionAbbr = o.CompositionAbbr,
                      CompositionOriginal = o.CompositionOriginal,
                      ProductType = o.ProductType,
                      Purity = o.Purity,
                      Quantity = o.Quantity,
                      QuantityUnit = o.QuantityUnit,
                      Dimension = o.Dimension,
                      DimensionDetails = o.DimensionDetails,
                      SampleNeed = o.SampleNeed,
                      DeadLine = o.DeadLine,
                      MinimumAcceptDefect = o.MinimumAcceptDefect,
                      OrderRemark = o.Remark,
                      Creator = p.Creator,
                      CreateTime = p.CreateTime,
                      PlanDate = p.PlanDate,
                      PlanLot = p.PlanLot,
                      VHPDeviceCode = p.VHPDeviceCode,
                      MoldType = p.MoldType,
                      CalculationDensity = p.CalculationDensity,
                      MoldDiameter = p.MoldDiameter,
                      Thickness = p.Thickness,
                      TargetQuantity = p.Quantity,
                      SingleWeight = p.SingleWeight,
                      AllWeight = p.AllWeight,
                      GrainSize = p.GrainSize,
                      RoomHumidity = p.RoomHumidity,
                      RoomTemperature = p.RoomTemperature,
                      PreTemperature = p.PreTemperature,
                      PrePressure = p.PrePressure,
                      Temperature = p.Temperature,
                      Pressure = p.Pressure,
                      Vaccum = p.Vaccum,
                      KeepTempTime = p.KeepTempTime,
                      ProcessCode = p.ProcessCode,
                      MillingRequirement = p.MillingRequirement,
                      FillingRequirement = p.FillingRequirement,
                      VHPPlanRemark = p.Remark,
                      VHPRequirement = p.VHPRequirement,
                      MachineRequirement = p.MachineRequirement,
                      SpecialRequirement = p.SpecialRequirement
                  }).ToList();
                    #endregion
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMissonWithPlanCheckedCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                where p.State == PMSCommon.CommonState.Checked.ToString()
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

        public List<DcOrder> GetMissonsBySearch(int skip, int take, string compostion)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType.Contains("VHP")
                                 && o.CompositionStandard.Contains(compostion)
                                 && (o.State == OrderState.UnCompleted.ToString()
                                 || o.State == OrderState.Paused.ToString()
                                 || o.State == OrderState.Completed.ToString())
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMissonsCountBySearch(string compostion)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.Orders
                                where o.PolicyType.Contains("VHP")
                                 && o.CompositionStandard.Contains(compostion)
                                 && (o.State == OrderState.UnCompleted.ToString()
                                 || o.State == OrderState.Paused.ToString()
                                 || o.State == OrderState.Completed.ToString())
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

        public List<DcMissonWithPlan> GetMissonWithPlanCheckedByDateRange(int skip, int take, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var startDate = dateStart.Date;
                    var endDate = dateEnd.Date;
                    //此处直接在linq中使用date属性会出错
                    #region 以后再简化
                    var result = dc.VHPPlans.Where(p => p.State == PMSCommon.CommonState.Checked.ToString()
                    && p.PlanDate >= startDate
                    && p.PlanDate <= endDate)
                    .OrderByDescending(p => p.PlanDate)
                    .Skip(skip).Take(take)
                    .Join(dc.Orders, p => p.OrderID, o => o.ID, (p, o) => new DcMissonWithPlan
                    {
                        OrderID = o.ID,
                        PlanID = p.ID,
                        CustomerName = o.CustomerName,
                        PO = o.PO,
                        PMINumber = o.PMINumber,
                        CompositionStandard = o.CompositionStandard,
                        CompositionAbbr = o.CompositionAbbr,
                        CompositionOriginal = o.CompositionOriginal,
                        ProductType = o.ProductType,
                        Purity = o.Purity,
                        Quantity = o.Quantity,
                        QuantityUnit = o.QuantityUnit,
                        Dimension = o.Dimension,
                        DimensionDetails = o.DimensionDetails,
                        SampleNeed = o.SampleNeed,
                        DeadLine = o.DeadLine,
                        MinimumAcceptDefect = o.MinimumAcceptDefect,
                        OrderRemark = o.Remark,
                        Creator = p.Creator,
                        CreateTime = p.CreateTime,
                        PlanDate = p.PlanDate,
                        PlanLot = p.PlanLot,
                        VHPDeviceCode = p.VHPDeviceCode,
                        MoldType = p.MoldType,
                        CalculationDensity = p.CalculationDensity,
                        MoldDiameter = p.MoldDiameter,
                        Thickness = p.Thickness,
                        TargetQuantity = p.Quantity,
                        SingleWeight = p.SingleWeight,
                        AllWeight = p.AllWeight,
                        GrainSize = p.GrainSize,
                        RoomHumidity = p.RoomHumidity,
                        RoomTemperature = p.RoomTemperature,
                        PreTemperature = p.PreTemperature,
                        PrePressure = p.PrePressure,
                        Temperature = p.Temperature,
                        Pressure = p.Pressure,
                        Vaccum = p.Vaccum,
                        KeepTempTime = p.KeepTempTime,
                        ProcessCode = p.ProcessCode,
                        MillingRequirement = p.MillingRequirement,
                        FillingRequirement = p.FillingRequirement,
                        VHPPlanRemark = p.Remark,
                        VHPRequirement = p.VHPRequirement,
                        MachineRequirement = p.MachineRequirement,
                        SpecialRequirement = p.SpecialRequirement
                    }).ToList();
                    #endregion
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetMissonWithPlanCheckedCountByDateRange(DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                var startDate = dateStart.Date;
                var endDate = dateEnd.Date;
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var query = from p in dc.VHPPlans
                                where p.State == PMSCommon.CommonState.Checked.ToString() && p.PlanDate >= startDate && p.PlanDate <= endDate
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

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<DcPlanWithMisson> GetPlanWithMisson(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State != PMSCommon.CommonState.Deleted.ToString()
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State != PMSCommon.CommonState.Deleted.ToString()
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 分页获取Checked状态的
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<DcPlanWithMisson> GetPlanWithMissonChecked(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.Checked.ToString()
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        public int GetPlanWithMissonCheckedCount()
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.Checked.ToString()
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanWithMissonCheckedByDateRange(int skip, int take, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                var startDate = dateStart.Date;
                var endDate = dateEnd.Date;
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.Checked.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p).Skip(skip).Take(take);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanWithMissonCheckedCountByDateRange(DateTime dateStart, DateTime dateEnd)
        {
            var startDate = dateStart.Date;
            var endDate = dateEnd.Date;
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var queryPlan = (from p in dc.VHPPlans
                                     where p.State == PMSCommon.CommonState.Checked.ToString()
                                     && p.PlanDate >= startDate
                                     && p.PlanDate <= endDate
                                     orderby p.PlanDate descending
                                     select p);
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                    return queryResult.Count();
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