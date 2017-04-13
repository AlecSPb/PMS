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
                    //var query = from p in dc.VHPPlans
                    //            join o in dc.Orders on p.OrderID equals o.ID
                    //            where p.State!=PMSCommon.CommonState.Deleted.ToString()
                    //            orderby p.PlanDate descending
                    //            select new DcMissonWithPlan()
                    //            {

                    //            };

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

        public List<DcMissonWithPlan> GetMissonWithPlanByDate(DateTime date)
        {
            try
            {
                using (var dc = new PMSDAL.PMSDbContext())
                {
                    var today = date.Date;
                    var tomorrow = date.Date.AddDays(1);
                    //此处直接在linq中使用date属性会出错
                    #region 以后再简化
                    var result = dc.VHPPlans.Where(p => p.PlanDate >= today && p.PlanDate < tomorrow)
                    .OrderByDescending(p => p.PlanDate)
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
    }
}