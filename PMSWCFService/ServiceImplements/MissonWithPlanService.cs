using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public partial class PMSService : IMissonWithPlanService
    {
        public List<DcMissonWithPlan> GetMissonWithPlan(int skip, int take)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                var result = dc.VHPPlans.OrderByDescending(p => p.PlanDate).OrderBy(p=>p.VHPDeviceCode).Skip(skip).Take(take)
                    .Join(dc.Orders, p => p.OrderID, o => o.ID, (p, o) => new DcMissonWithPlan()
                    {
                        OrderID = o.ID,
                        PlanID = p.ID,
                        CustomerName = o.CustomerName,
                        PO = o.PO,
                        PMIWorkingNumber = o.PMIWorkingNumber,
                        CompositionStandard = o.CompositionStandard,
                        CompositoinAbbr = o.CompositoinAbbr,
                        CompositionOriginal = o.CompositionOriginal,
                        ProductType = o.ProductType,
                        Purity = o.Purity,
                        Quantity = o.Quantity ?? 0,
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
                        VHPDeviceCode = p.VHPDeviceCode,
                        MoldType = p.MoldType,
                        CalculationDensity = p.CalculationDensity,
                        MoldDiameter = p.MoldDiameter,
                        Thickness = p.Thickness,
                        TargetQuantity = p.Quantity,
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

                return result;
            }
        }

        public List<DcMissonWithPlan> GetMissonWithPlanByDate(DateTime date)
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                var today = date.Date;
                var tomorrow = date.Date.AddDays(1);
                //此处直接在linq中使用date属性会出错
                var result = dc.VHPPlans.Where(p => p.PlanDate >= today && p.PlanDate < tomorrow).OrderByDescending(p => p.PlanDate)
                    .Join(dc.Orders, p => p.OrderID, o => o.ID, (p, o) => new DcMissonWithPlan
                    {
                        OrderID = o.ID,
                        PlanID = p.ID,
                        CustomerName = o.CustomerName,
                        PO = o.PO,
                        PMIWorkingNumber = o.PMIWorkingNumber,
                        CompositionStandard = o.CompositionStandard,
                        CompositoinAbbr = o.CompositoinAbbr,
                        CompositionOriginal = o.CompositionOriginal,
                        ProductType = o.ProductType,
                        Purity = o.Purity,
                        Quantity = o.Quantity ?? 0,
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
                        VHPDeviceCode = p.VHPDeviceCode,
                        MoldType = p.MoldType,
                        CalculationDensity = p.CalculationDensity,
                        MoldDiameter = p.MoldDiameter,
                        Thickness = p.Thickness,
                        TargetQuantity = p.Quantity,
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


                return result;
            }
        }

        public int GetMissonWithPlanCount()
        {
            using (var dc = new PMSDAL.PMSDbContext())
            {
                return dc.VHPPlans.Count();
            }
        }
    }
}