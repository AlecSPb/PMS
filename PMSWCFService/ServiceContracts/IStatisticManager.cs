using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.ServiceContracts
{
    /// <summary>
    /// 生产经理超级视角服务
    /// 
    /// </summary>
    [ServiceContract]
    public interface IStatisticManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string,List<string>> GetStatisticData(DateTime start, DateTime end);

    }
}
