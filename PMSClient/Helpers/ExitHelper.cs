using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.Helpers
{
    public class ExitHelper
    {
        public void ProcessWhenExitApp()
        {
            WarningMaterialOrderUnChecked();
        }

        private void WarningMaterialOrderUnChecked()
        {
            //如果当前用户有编辑原料订单的权限，而且存在没有核验的原料订单，在退出程序的时候进行提示
            if (PMSHelper.CurrentSession != null && PMSHelper.CurrentSession.CurrentAccesses != null)
            {

            }
            bool checker1 = PMSHelper.CurrentSession.CurrentAccesses.Where(i => i.AccessName == PMSAccess.EditMaterialOrder).Count() > 0;
            bool checker2 = CheckMaterialOrderUnChecked();
            if (checker1 && checker2)
            {
                PMSDialogService.ShowYes("提示：原料订单里有未核验的订单！\r\n未核验订单供应商无法看到");
            }
        }

        private bool CheckMaterialOrderUnChecked()
        {
            using (var service = new MaterialOrderServiceClient())
            {
                return service.CheckMaterialOrderUnChecked();
            }
        }
    }
}
