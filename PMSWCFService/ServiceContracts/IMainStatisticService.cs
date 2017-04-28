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
    public interface IMainStatisticService
    {
        [OperationContract]
        List<DcStatistic> GetOrderStatisticByYear();
        [OperationContract]
        List<DcStatistic> GetOrderStatisticBySeason();
        [OperationContract]
        List<DcStatistic> GetOrderStatisticByMonth();
        [OperationContract]
        List<DcStatistic> GetOrderStatisticByCustomer();





        [OperationContract]
        List<DcStatistic> GetMissonStatistic();

        [OperationContract]
        List<DcStatistic> GetPlanStatisticByYear();
        [OperationContract]
        List<DcStatistic> GetPlanStatisticByMonth();
        [OperationContract]
        List<DcStatistic> GetPlanStatisticBySeaon();
        [OperationContract]
        List<DcStatistic> GetPlanStatisticByDevice();


        [OperationContract]
        List<DcStatistic> GetDeliveryByYear();
        [OperationContract]
        List<DcStatistic> GetDeliveryBySeaon();
        [OperationContract]
        List<DcStatistic> GetDeliveryByMonth();
    }
}
