using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IDeliveryAddressService
    {
        [OperationContract]
        List<DcBDDeliveryAddress> GetDeliveryAddress();
        [OperationContract]
        int AddDeliveryAddress(DcBDDeliveryAddress model);
        [OperationContract]
        int UpdateDeliveryAddress(DcBDDeliveryAddress model);
        [OperationContract]
        int DeleteDeliveryAddress(Guid id);
    }
}