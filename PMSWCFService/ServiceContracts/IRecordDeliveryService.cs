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
    public interface IDeliveryService
    {
        [OperationContract]
        List<DcDelivery> GetDelivery(int skip, int take);
        [OperationContract]
        int GetDeliveryCount();

        [OperationContract]
        int AddDelivery(DcDelivery model);
        [OperationContract]
        int UpdateDelivery(DcDelivery model);
        [OperationContract]
        int DeleteDelivery(Guid id);

        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemByDeliveryID(Guid id);
        [OperationContract]
        int AddDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int UpdateDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int DeleteDeliveryItem(Guid id);


    }
}
