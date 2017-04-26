﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public static class PMSNewModelCollection
    {
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