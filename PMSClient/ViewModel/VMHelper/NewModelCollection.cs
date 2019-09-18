using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public static class PMSNewModelCollection
    {
        public static DcRecordTest NewRecordTest()
        {
            var model = new DcRecordTest();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            model.FollowUps = "发货";
            model.Composition = "成分";
            model.ProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.CompositionXRF = "暂无";
            model.Dimension = "要求尺寸";
            model.DimensionActual = "实际尺寸";
            model.PO = "PO";
            model.CompositionAbbr = "成分缩写";
            model.Customer = "客户信息";
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.TestType = PMSCommon.TestType.靶材.ToString();
            model.State = PMSCommon.CommonState.未录完.ToString();
            model.Weight = "0";
            model.Remark = "";
            model.Resistance = "0";
            model.Sample = "";
            model.CompositionXRF = "暂无";
            model.Density = "0";
            model.Defects = "无";
            model.OrderDate = DateTime.Now.AddDays(-30);
            model.Roughness = "无";
            model.Warping = "未知";
            model.QC = "无";
            model.BackingPlateLot = "无";
            return model;
        }
        public static DcRecordMachine NewRecordMachine()
        {
            var model = new DcRecordMachine();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "成分";
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Dimension = "230mm OD x 4mm";
            model.BlankDimension = "无";
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.ExtraRequirement = "正常要求";
            model.Defects = PMSCommon.TestDefectsTypes.无缺陷.ToString();
            return model;
        }
        public static DcRecordDeMold NewRecordDeMold()
        {
            var model = new DcRecordDeMold();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.DeMoldType = PMSCommon.DeMoldType.手动轻松.ToString();
            model.PlanType = PMSCommon.VHPPlanType.加工.ToString();
            model.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            model.Dimension = "无";
            model.CalculateDimension = "无";
            model.CalculationDensity = 0;
            model.Density = 0;
            model.RatioDensity = 0;
            model.Composition = "成分";
            model.Temperature1 = "0";
            model.Temperature2 = "0";
            model.Weight = 0;
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.Remark = "无";
            return model;
        }
        public static DcRecordMilling NewRecordMilling()
        {
            var model = new DcRecordMilling();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Water = "无";
            model.Oxygen = "无";
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.PlanBatchNumber = 0;
            model.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            model.RoomHumidity = 0;
            model.RoomTemperature = 0;
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "填入成分";
            model.GasProtection = PMSCommon.MillingGas.Ar气.ToString();
            model.MaterialSource = PMSCommon.MillingMaterialSource.SJ.ToString();
            model.MillingTool = PMSCommon.MillingTool.行星球磨.ToString();
            model.MillingTime = "无";
            model.Remark = "";
            model.WeightIn = 0;
            model.WeightOut = 0;
            model.WeightRemain = 0;
            model.Ratio = 0;
            model.MeltingPoint = "无";
            model.Details = "";
            return model;
        }
        public static DcMaterialOrderItem NewMaterialOrderItem(DcMaterialOrder order)
        {
            var item = new DcMaterialOrderItem();
            #region 初始化
            item.ID = Guid.NewGuid();
            item.MaterialOrderID = order.ID;
            item.State = PMSCommon.MaterialOrderItemState.未完成.ToString();
            item.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            item.CreateTime = DateTime.Now;
            item.OrderItemNumber = DateTime.Now.ToString("yyMMdd") + order.SupplierAbbr + 1;
            item.Composition = "需求成分";
            item.PMINumber = DateTime.Now.ToString("yyMMdd");
            item.Purity = "5N";
            item.Description = "";
            item.ProvideRawMaterial = "";
            item.UnitPrice = 0;
            item.Weight = 0;
            item.DeliveryDate = DateTime.Now.AddDays(7);
            item.Priority = PMSCommon.MaterialOrderItemPriority.普通.ToString();
            item.SJIngredient = "无";
            item.Remark = "";
            #endregion
            return item;
        }

        public static DcRecordBonding NewRecordBonding()
        {
            var model = new DcRecordBonding();
            #region 初始化
            model.ID = Guid.NewGuid();
            //0.0
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.BondingState.未完成.ToString();
            model.InstructionCode = "无";
            model.TargetProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.TargetComposition = "靶材成分";
            model.TargetDimension = "靶材尺寸";
            model.PlateType = "新背板";
            model.CoverPlateNumber = "无";
            model.PlanBatchNumber = 1;
            model.WeldingRate = 0;

            //暂时用不到
            model.TargetAbbr = "尚未确定";
            model.TargetPO = "尚未确定";
            model.TargetPMINumber = UsefulPackage.PMSTranslate.PMINumber();
            model.TargetWeight = "尚未确定";
            model.TargetDimensionActual = "尚未确定";
            model.TargetDefects = "尚未确定";
            model.TargetDetailRecord = "尚未确定";
            //暂时用不到
            //1.0
            model.TargetAppearance = "正常";
            model.TargetWarpageCheck = "正常";
            model.TargetThicknessCheck = "正常";
            model.TargetDiameterCheck = "正常";
            model.TargetPerson = "无";
            model.TargetCheckTime = DateTime.Now;

            //2.0
            model.PlateLot = "暂无";
            model.PlateMaterial = "CuCr";
            model.PlateDimension = "237mm  ODx 11 mm";
            model.PlateUseCount = "0";
            model.PlateHardness = "未知";
            model.PlateSuplier = "广汉";
            model.PlateLastWeldMaterial = "无";
            model.PlateAppearance = "正常";
            model.PlatePerson = "无";
            model.PlateCheckTime = DateTime.Now;

            //3.0
            model.TargetPreProcessRecord = "正常";
            model.TargetPreProcessPerson = "无";
            model.TargetPreProcessCheckTime = DateTime.Now;

            //4.0
            model.PlatePreProcessRecord = "正常";
            model.PlatePreProcessPerson = "无";
            model.PlatePreProcessCheckTime = DateTime.Now;

            //5.0
            model.WeldMaterial = "铟";
            model.WeldCuStringDiameter = "3.0";
            model.WeldHold = "4个";
            model.WeldPerson = "无";
            model.WeldCheckTime = DateTime.Now;
            //6.0
            model.WarpageFix = "正常";
            model.WarpagePerson = "无";
            model.WarpageCheckTime = DateTime.Now;
            //7.0
            model.DimensionCheck = "正常";
            model.DimensionWarpageCheck = "无";
            model.DimensionPerson = "无";
            model.DimensionCheckTime = DateTime.Now;
            //
            model.BindingCheck = "正常";
            model.BindingPerson = "无";
            model.BindingCheckTime = DateTime.Now;
            //
            model.SprayCheck = "正常";
            model.SprayPerson = "无";
            model.SprayCheckTime = DateTime.Now;
            //
            model.CleanCheck = "正常";
            model.CleanPerson = "无";
            model.CleanCheckTime = DateTime.Now;
            //
            model.ApperanceCheck = "正常";
            model.ApperancePerson = "无";
            model.ApperanceCheckTime = DateTime.Now;
            //
            model.PackCheck = "正常";
            model.PackPerson = "无";
            model.PackCheckTime = DateTime.Now;

            model.Remark = "无";


            #endregion
            return model;
        }
        public static DcProduct NewProduct()
        {
            var model = new DcProduct();
            model.ID = Guid.NewGuid();
            model.ProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Composition = "成分";
            model.Abbr = "缩写";
            model.Weight = "";
            model.Customer = "客户";
            model.Position = PMSCommon.GoodPosition.A2.ToString();
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.Remark = "";

            model.Dimension = "尺寸";
            model.DimensionActual = "实际尺寸";
            model.Defects = "无";
            return model;
        }
        public static DcDeliveryItem NewDeliveryItem(Guid deliveryid)
        {

            var model = new DcDeliveryItem();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.DeliveryID = deliveryid;
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.ProductID = DateTime.Now.ToString("yyMMdd");
            model.Composition = "填写成分";
            model.Abbr = "缩写";
            model.PO = "PO";
            model.Customer = "客户";
            model.Weight = "重量";
            model.DetailRecord = "细节";
            model.Remark = "无";
            model.PackNumber = 1;
            model.Position = "B2";
            model.Dimension = "";
            model.DimensionActual = "";
            model.Defects = "无";
            model.State = PMSCommon.SimpleState.正常.ToString();
            return model;
        }
    }
}
