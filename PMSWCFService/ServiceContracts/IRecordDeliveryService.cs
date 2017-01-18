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
        List<DcRecordDelivery> GetDeliveryBySearchInPage(int skip, int take, string searchDeliveryID);
        [OperationContract]
        int AddRecordDelivery(DcRecordDelivery model);
        [OperationContract]
        int UpdateReocrdDelivery(DcRecordDelivery model);
        [OperationContract]
        int DeleteRecordDelivery(Guid id);
        [OperationContract]
        int AddRecordDeliveryItem(DcRecordDeliveryItem model);
        [OperationContract]
        int UpdateReocrdDeliveryItem(DcRecordDeliveryItem model);
        [OperationContract]
        int DeleteRecordDeliveryItem(Guid id);


    }
}
