using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.DataProcess.ScanInput
{
    public static class ModelHelper
    {
        public static DcRecordBonding GetRecordBonding(DcRecordTest ss, int batchNumber, string plate)
        {
            if (ss == null)
                return null;
            #region 初始化
            var model = new DcRecordBonding();
            model.ID = Guid.NewGuid();
            //0.0
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.BondingState.未完成.ToString();
            model.PlanBatchNumber = batchNumber;
            model.InstructionCode = "无";
            model.TargetProductID = ss.ProductID;
            model.TargetComposition = ss.Composition;
            model.TargetDimension = ss.Dimension;
            model.PlateType = plate;
            model.CoverPlateNumber = "无";
            //暂时用不到
            model.TargetAbbr = ss.CompositionAbbr;
            model.TargetPO = ss.PO;
            model.TargetPMINumber = ss.PMINumber;
            model.TargetWeight = ss.Weight;
            model.TargetDimensionActual = ss.DimensionActual;
            model.TargetDefects = ss.Defects;
            model.TargetDetailRecord = "";
            //暂时用不到
            //1.0
            model.TargetAppearance = "正常";
            model.TargetWarpageCheck = "正常";
            model.TargetThicknessCheck = "正常";
            model.TargetDiameterCheck = "正常";
            model.TargetPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
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
            model.PlatePerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.PlateCheckTime = DateTime.Now;

            //3.0
            model.TargetPreProcessRecord = "正常";
            model.TargetPreProcessPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.TargetPreProcessCheckTime = DateTime.Now;

            //4.0
            model.PlatePreProcessRecord = "正常";
            model.PlatePreProcessPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.PlatePreProcessCheckTime = DateTime.Now;

            //5.0
            model.WeldMaterial = "铟";
            model.WeldCuStringDiameter = "3.0";
            model.WeldHold = "4个";
            model.WeldPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.WeldCheckTime = DateTime.Now;
            //6.0
            model.WarpageFix = "正常";
            model.WarpagePerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.WarpageCheckTime = DateTime.Now;
            //7.0
            model.DimensionCheck = "正常";
            model.DimensionWarpageCheck = "无";
            model.DimensionPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.DimensionCheckTime = DateTime.Now;
            //
            model.BindingCheck = "正常";
            model.BindingPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.BindingCheckTime = DateTime.Now;
            //
            model.SprayCheck = "正常";
            model.SprayPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.SprayCheckTime = DateTime.Now;
            //
            model.CleanCheck = "正常";
            model.CleanPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.CleanCheckTime = DateTime.Now;
            //
            model.ApperanceCheck = "正常";
            model.ApperancePerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.ApperanceCheckTime = DateTime.Now;
            //
            model.PackCheck = "正常";
            model.PackPerson = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.PackCheckTime = DateTime.Now;

            model.Remark = "";
            #endregion
            return model;
        }

        public static DcProduct GetProduct(DcRecordTest ss)
        {
            if (ss == null)
                return null;
            var model = new DcProduct();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.ProductID = ss.ProductID;
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Composition = ss.Composition;
            model.Abbr = ss.CompositionAbbr;
            model.Weight = ss.Weight;
            model.Customer = ss.Customer;
            model.Position = PMSCommon.GoodPosition.A1.ToString();
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.Remark = "";
            model.PO = ss.PO;
            model.Dimension = ss.Dimension;
            model.DimensionActual = ss.DimensionActual;
            model.Defects = ss.Defects;
            #endregion
            return model;

        }


        public static DcDeliveryItem GetDeliveryItem(DcPlate ss, int boxNumber)
        {
            if (ss == null)
                return null;
            var model = new DcDeliveryItem();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.ProductType = PMSCommon.ProductType.背板.ToString();
            model.ProductID = ss.PlateLot;
            model.Composition = ss.PlateMaterial;
            model.Abbr = ss.PlateMaterial;
            model.PO = "";
            model.Customer = "无";
            model.Weight = ss.Weight;
            model.DetailRecord = "无";
            model.Remark = "无";
            model.PackNumber = boxNumber;
            model.Position = "无";
            model.Dimension = ss.Dimension;
            model.DimensionActual = ss.Dimension;
            model.Defects = ss.Defects;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.OrderNumber = 0;

            #endregion

            return model;
        }

        public static DcDeliveryItem GetDeliveryItem(DcProduct ss)
        {
            if (ss == null)
                return null;
            var model = new DcDeliveryItem();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.ProductType = ss.ProductType;
            model.ProductID = ss.ProductID;
            model.Composition = ss.Composition;
            model.Abbr = ss.Abbr;
            model.PO = ss.PO;
            model.Customer = ss.Customer;
            model.Weight = ss.Weight;
            model.DetailRecord = "无";
            model.Remark = "无";
            model.PackNumber = 1;
            model.Position = "无";
            model.Dimension = ss.Dimension;
            model.DimensionActual = ss.DimensionActual;
            model.Defects = ss.Defects;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.OrderNumber = 0;

            #endregion

            return model;
        }

    }
}
