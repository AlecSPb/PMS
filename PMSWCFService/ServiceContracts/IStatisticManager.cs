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
    /// </summary>
    [ServiceContract]
    public interface IStatisticManager
    {
        [OperationContract]
        int GetPlanCount(DateTime start, DateTime end,string deviceCode);

        [OperationContract]
        Dictionary<string, string> GetMoldUsage(DateTime start, DateTime end);

        [OperationContract]
        Dictionary<string, string> GetVHPType(DateTime start, DateTime end);

        [OperationContract]
        Dictionary<string, string> GetFollowUp(DateTime start, DateTime end);

        [OperationContract]
        int GetBondingCount(DateTime start, DateTime end);

        [OperationContract]
        int GetTestingCount(DateTime start, DateTime end);

        [OperationContract]
        int GetDeliveryCount(DateTime start, DateTime end);
    }
}
