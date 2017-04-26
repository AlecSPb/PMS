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
        public static DcRecordBonding NewRecordBonding()
        {
            var model = new DcRecordBonding();
            #region 初始化
            model.ID = Guid.NewGuid();
            //0.0
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.CommonState.未核验.ToString();
            model.InstructionCode = "无";
            model.TargetProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.TargetComposition = "靶材成分";
            model.TargetDimension = "靶材尺寸";
            //暂时用不到
            model.TargetAbbr = "";
            model.TargetPO = "";
            model.TargetPMINumber = UsefulPackage.PMSTranslate.PMINumber();
            model.TargetWeight = "";
            model.TargetDimensionActual = "";
            model.TargetDefects = "";
            model.TargetDetailRecord = "";
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
            model.PlateUseCount = "1";
            model.PlateHardness = "未知";
            model.PlateSuplier = "广汉";
            model.PlateLastWeldMaterial = "铟";
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

            model.Remark = "";


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
            model.Position = PMSCommon.GoodPosition.A1.ToString();
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
            model.Position = "A2";
            model.Dimension = "";
            model.DimensionActual = "";
            model.Defects = "无";
            model.State = PMSCommon.SimpleState.正常.ToString();
            return model;
        }
    }
}
