using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient
{
    public static class ModelInitializer
    {
        public static  DcMaterialNeed EmptyMaterialNeed
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
            empty.State = PMSCommon.NonOrderState.UnDeleted.ToString();
            empty.CreateTime = DateTime.Now;
            empty.Creator = (App.Current as App).CurrentUser.UserName;
            empty.Purity = "5N";
            empty.Weight = 1.0;
            empty.PMIWorkingNumber = order.PMIWorkingNumber;
            empty.Composition = order.CompositionStandard;
            return empty;
        }
          
    }
}
