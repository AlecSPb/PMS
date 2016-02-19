using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface IMainPlanService
    {
        /// <summary>
        /// 按照MainOrderId查找所有的MainPlan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<MainPlan> GetMainPlansByMainOrderId(Guid id);

        [OperationContract]
        bool AddMainPlan(MainPlan plan);
        [OperationContract]
        bool UpdateMainPlan(MainPlan plan);
        [OperationContract]
        bool DeleteMainPlan(MainPlan plan);





    }
}
