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
    public interface IMissonService
    {
        [OperationContract]
        List<DcOrder> GetMissons(int skip,int take);
        [OperationContract]
        int GetMissonsCount();

        [OperationContract]
        List<DcOrder> GetMissonsBySearch(int skip, int take,string compostion);
        [OperationContract]
        int GetMissonsCountBySearch(string compostion);


        [OperationContract]
        List<DcPlanVHP> GetPlans(Guid id);



        //所有状态的计划
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlan(int skip, int take);
        [OperationContract]
        int GetMissonWithPlanCount();


        //Checked状态的计划显示
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlanChecked(int skip, int take);
        [OperationContract]
        int GetMissonWithPlanCheckedCount();


        //按照日期获取
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlanByDate(DateTime date);
    }
}
