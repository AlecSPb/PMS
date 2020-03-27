using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using PMSClient.OutsideProcessService;

namespace PMSClient.DataProcess.ScanInput
{
    public static class ModelHelper
    {

        public static DcOutsideProcess GetOutsideProcess(DcRecordTest ss,string provider)
        {
            if (ss == null) return null;
            var model = new DcOutsideProcess();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator= PMSHelper.CurrentSession.CurrentUser.UserName;
            model.ProductID = ss.ProductID;
            model.Composition = ss.Composition;
            model.Customer = ss.Customer;
            model.Dimension = ss.Dimension;
            model.PMINumber = ss.PMINumber;
            model.PONumber = ss.PO;
            model.Processor = provider;
            model.State = PMSCommon.OutsideProcessState.待发出.ToString();
            model.ProgressBar = "";
            model.Remark = "";
            return model;
        }

        public static DcRecordBonding GetRecordBonding(DcRecordTest ss, int batchNumber, string plate)
        {
            if (ss == null)
                return null;
            #region 初始化
            var model = new DcRecordBonding();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.BondingState.未完成.ToString();
            model.PlanBatchNumber = batchNumber;
            model.TargetProductID = ss.ProductID;
            model.TargetComposition = ss.Composition;
            model.TargetCustomer = ss.Customer;
            model.TargetDimension = ss.Dimension;
            model.PlateType = plate;
            model.CoverPlateNumber = "无";
            model.TargetAbbr = ss.CompositionAbbr;
            model.TargetPO = ss.PO;
            model.TargetPMINumber = ss.PMINumber;
            model.TargetWeight = ss.Weight;
            model.TargetDimensionActual = ss.DimensionActual;
            model.TargetDefects = ss.Defects;
            model.TargetDetailRecord = "";
            model.PlateLot = "暂无";

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
            model.Position = "无";
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
            model.PackNumber = boxNumber;
            model.Position = "无";
            model.Dimension = ss.Dimension;
            model.DimensionActual = ss.Dimension;
            model.Defects = ss.Defects;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.OrderNumber = 0;

            model.Remark = "无";
            #endregion

            return model;
        }

        public static DcDeliveryItem GetDeliveryItem(DcProduct ss, int boxNumber = 1)
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
            model.DetailRecord = "细节";
            model.PackNumber = boxNumber;
            model.Position = "无";
            model.Dimension = ss.Dimension;
            model.DimensionActual = ss.DimensionActual;
            model.Defects = ss.Defects;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.OrderNumber = 0;
            model.Remark = "无";

            //TODO:背板编号的处理
            if (model.ProductType == PMSCommon.ProductType.靶材.ToString())
            {
                var platelot = Helpers.DeliveryHelper.GetBPLotFromTest(model.ProductID);
                model.Remark = platelot;
            }
            #endregion

            return model;
        }

    }
}
