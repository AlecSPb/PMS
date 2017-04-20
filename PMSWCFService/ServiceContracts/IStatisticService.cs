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
    public interface IStatisticService
    {
        [OperationContract]
        DcStatisticOrder GetStatisticOrder();
        [OperationContract]
        DcStatisticPlan GetStatisticPlan();
        [OperationContract]
        DcStatisticProduct GetStatisticProduct();
        [OperationContract]
        DcStatisticDelivery GetStatisticDelivery();
    }
}
