using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient
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
            empty.State = PMSCommon.NoneOrderState.UnDeleted.ToString();
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
    }
}
