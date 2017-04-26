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
