using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.ViewModel.VMHelper
{
    public class RecordVHPVMHelper
    {
        public static void LockAll()
        {
            try
            {

                var plandate = DateTime.Today.ToString("yyMMdd");
                var misson_service = new MissonServiceClient();
                var today_plans = misson_service.GetPlanExtra(0, 100, plandate, string.Empty);
                var plan_service = new PlanVHPServiceClient();
                foreach (var item in today_plans)
                {
                    if (!item.Plan.IsLocked)
                    {
                        var plan = item.Plan;
                        plan.IsLocked = true;
                        plan_service.UpdateVHPPlan(plan);
                    }
                }
                plan_service.Close();
                misson_service.Close();
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }
    }
}
