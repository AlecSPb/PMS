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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMainPlan(MainPlan plan);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMainPlan(MainPlan plan);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMainPlan(MainPlan plan);





    }
}
