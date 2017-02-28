using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSLargeScreen.PMSMainService;

namespace PMSLargeScreen
{
    public static class LargeScreenService
    {
        public static List<DcMissonWithPlan> GetTodayMissonWithPlan()
        {
            var today = DateTime.Now;
            using (var service = new MissonWithPlanServiceClient())
            {
                var result = service.GetMissonWithPlanByDate(today);
                return result.ToList();
            }
        }
    }
}
