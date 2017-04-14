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
        //获取任务
        [OperationContract]
        List<DcOrder> GetMissons(int skip,int take);
        [OperationContract]
        int GetMissonsCount();


        [OperationContract]
        List<DcOrder> GetMissonsBySearch(int skip, int take,string compostion);
        [OperationContract]
        int GetMissonsCountBySearch(string compostion);

        /// <summary>
        /// 按任务id获取Plan，和IPlanVHP契约重复，后面考虑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        //按照日期范围获取
        [OperationContract]
        List<DcMissonWithPlan> GetMissonWithPlanCheckedByDateRange(int skip, int take,DateTime dateStart,DateTime dateEnd);
        [OperationContract]
        int GetMissonWithPlanCheckedCountByDateRange(DateTime dateStart, DateTime dateEnd);
    }
}
