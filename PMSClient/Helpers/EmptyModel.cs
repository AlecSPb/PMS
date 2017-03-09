using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.PMSMainService;

namespace PMSClient
{
    public static class EmptyModel
    {
        public static DcMaterialNeed EmptyMaterialNeed
        {
            get
            {
                var empty = new DcMaterialNeed();

                return empty;
            }
        }

        public static DcMaterialNeed GetMaterialNeedByOrder(DcOrder order)
        {
            var empty = new DcMaterialNeed();
            empty.Id = Guid.NewGuid();
            empty.State = PMSCommon.SimpleState.UnDeleted.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = (App.Current as App).CurrentUser.UserName;
            empty.Purity = "5N";
            empty.Weight = 1.0;
            empty.PMIWorkingNumber = order.PMIWorkingNumber;
            empty.Composition = order.CompositionStandard;
            return empty;
        }


        public static DcOrder GetOrder()
        {
            var dcOrder = new DcOrder();
            dcOrder.ID = Guid.NewGuid();
            dcOrder.CustomerName = "Midsummer";
            dcOrder.PO = DateTime.Now.ToString("yyMMdd");
            dcOrder.PMIWorkingNumber = DateTime.Now.ToString("yyMMdd");
            dcOrder.ProductType = "Target";
            dcOrder.Dimension = "230mm OD x  4mm";
            dcOrder.DimensionDetails = "None";
            dcOrder.SampleNeed = "无需样品";
            dcOrder.MinimumAcceptDefect = "通常";
            dcOrder.Reviewer = "xs.zhou";
            dcOrder.PolicyContent = "";
            dcOrder.PolicyType = "VHP";
            dcOrder.PolicyMaker = "xs.zhou";

            dcOrder.Purity = "99.99";
            dcOrder.DeadLine = DateTime.Now.AddDays(30);
            dcOrder.ReviewDate = DateTime.Now;
            dcOrder.PolicyMakeDate = DateTime.Now;
            dcOrder.State = "UnChecked";
            dcOrder.Priority = "Normal";
            dcOrder.CompositionOriginal = "CuGaSe2";
            dcOrder.CompositionStandard = "Cu25Ga25Se50";
            dcOrder.CompositoinAbbr = "CuGaSe";
            dcOrder.Creator = "xs.zhou";
            dcOrder.CreateTime = DateTime.Now;
            dcOrder.ProductType = "Target";
            dcOrder.ReviewPassed = true;
            dcOrder.Quantity = 1;
            dcOrder.QuantityUnit = "片";

            return dcOrder;
        }


        public static DcPlanVHP GetPlanVHP(DcOrder order)
        {
            DcPlanVHP plan = new DcPlanVHP();
            plan.ID = Guid.NewGuid();
            plan.OrderID = order.ID;
            plan.PlanDate = DateTime.Now.Date;
            plan.MoldType = "GQ";
            plan.VHPDeviceCode = "A";
            plan.Temperature = 0;
            plan.Pressure = 0;
            plan.Vaccum = 0;
            plan.ProcessCode = "W1";
            plan.PrePressure = 0;
            plan.PreTemperature = 25;
            plan.Quantity = 1;
            plan.MoldDiameter = 230;
            plan.Thickness = 5;
            plan.CreateTime = DateTime.Now;
            plan.State = "UnChecked";
            plan.CalculationDensity = 5.75;
            plan.GrainSize = "-200";
            plan.RoomHumidity = 70;
            plan.RoomTemperature = 23;
            plan.KeepTempTime = 120;
            plan.MillingRequirement = "常规要求";
            plan.MachineRequirement = "常规要求";
            plan.FillingRequirement = "常规要求";
            plan.SpecialRequirement = "无";
            plan.Creator = "xs.zhou";
            return plan;
        }

        public static DcMaterialOrder GetMaterialOrder()
        {
            var model = new DcMaterialOrder();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.State = PMSCommon.OrderState.UnChecked.ToString();
            model.Creator = (App.Current as App).CurrentUser.UserName;
            model.Supplier = "Sanjie";
            model.SupplierAbbr = "SJ";
            model.SupplierEmail = "sj_materials@163.com";
            model.SupplierReceiver = "Mr.Wang";
            model.SupplierAddress = "Chengdu,Sichuan CHINA";
            model.ShipFee = 0;
            model.Priority = PMSCommon.OrderPriority.Normal.ToString();
            model.Remark = "";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + "_" + model.SupplierAbbr;
            return model;
        }

        public static DcMaterialOrderItem GetMaterialOrderItemBy(DcMaterialOrder order)
        {
            var item = new PMSMainService.DcMaterialOrderItem();
            item.ID = Guid.NewGuid();
            item.MaterialOrderID = order.ID;
            item.State = PMSCommon.SimpleState.UnDeleted.ToString();
            item.Creator = (App.Current as App).CurrentUser.UserName;
            item.CreateTime = DateTime.Now;
            item.Composition = "Composition";
            item.PMIWorkNumber = "WorkNumber";
            item.Purity = "Purity";
            item.Description = "";
            item.ProvideRawMaterial = "";
            item.UnitPrice = 0;
            item.Weight = 0;
            item.DeliveryDate = DateTime.Now.AddDays(7);
            return item;
        }

    }
}
