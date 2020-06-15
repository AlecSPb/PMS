using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.NewService;

namespace PMSClient.ViewModel.VMHelper
{
    public class MissonVMHelper
    {
        public static void UpdateSpecialRequirement(DcPlanVHP obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "要对该计划进行转单操作吗？ 当前计划用于其他订单"))
            {
                if (obj == null) return;

                var dialog = new ToolWindow.SingleValueDialog();
                dialog.Value = "CD";
                dialog.ShowDialog();
                if (dialog.DialogResult == true)
                {
                    obj.SpecialRequirement = dialog.Value;
                    try
                    {
                        using (var s = new NewServiceClient())
                        {
                            s.UpdatePlan(obj, PMSHelper.CurrentSession.CurrentUser.UserName);
                        }
                        PMSDialogService.Show("转单成功");
                    }
                    catch (Exception ex)
                    {
                        PMSHelper.CurrentLog.Error(ex);
                    }
                }
            }
        }
    }
}
