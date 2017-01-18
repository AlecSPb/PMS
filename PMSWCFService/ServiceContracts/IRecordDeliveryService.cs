using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordDeliveryService
    {
        [OperationContract]
        List<DcRecordDelivery> GetDeliveryBySearchInPage(int skip, int take, string productId, string customer, string compositon);


    }
}
