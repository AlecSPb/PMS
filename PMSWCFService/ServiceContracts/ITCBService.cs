using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ITCBService
    {
        [OperationContract]
        List<DcDeliveryItemTCB> GetDeliveryItemTCB(int s, int t, string productid, string composition, string po, string customer,string bondingpo);

        [OperationContract]
        int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo);

        [OperationContract]
        void AddDeliveryItemTCB(DcDeliveryItemTCB model);
        [OperationContract]
        void UpdateDeliveryItemTCB(DcDeliveryItemTCB model);

    }
}