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
        List<DcStatistic> GetOrderStatisticBySeason(int year);
        [OperationContract]
        List<DcStatistic> GetOrderStatisticByMonth(int year);
        [OperationContract]
        List<DcStatistic> GetOrderStatisticByCustomer(int year);





        [OperationContract]
        List<DcStatistic> GetMissonStatistic();

        [OperationContract]
        List<DcStatistic> GetPlanStatisticByYear();
        [OperationContract]
        List<DcStatistic> GetPlanStatisticByMonth(int year);
        [OperationContract]
        List<DcStatistic> GetPlanStatisticBySeason(int year);
        [OperationContract]
        List<DcStatistic> GetPlanStatisticByDevice(int year);


        [OperationContract]
        List<DcStatistic> GetDeliveryStatisticByYear();
        [OperationContract]
        List<DcStatistic> GetDeliveryStatisticBySeason(int year);
        [OperationContract]
        List<DcStatistic> GetDeliveryStatisticByMonth(int year);
        [OperationContract]
        List<DcStatistic> GetDeliveryStatisticByProductType(int year);
        [OperationContract]
        List<DcStatistic> GetDeliveryStatisticByCustomer(int year);



        [OperationContract]
        List<DcStatistic> GetProductStatisticByYear();
        [OperationContract]
        List<DcStatistic> GetProductStatisticByMonth(int year);
        [OperationContract]
        List<DcStatistic> GetProductStatisticBySeason(int year);
        [OperationContract]
        List<DcStatistic> GetProductStatisticByProductType(int year);


    }
}
