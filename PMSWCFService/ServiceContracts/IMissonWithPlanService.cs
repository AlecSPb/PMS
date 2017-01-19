using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IMissonWithPlanService
    {
        //全部
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlan(int skip, int take);
        [OperationContract]
        int GetMissonWithPlanCount();

        //按照日期获取
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlanByDate(DateTime date);


    }
}
