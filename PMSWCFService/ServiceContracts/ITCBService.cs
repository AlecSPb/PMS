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
        List<DcDeliveryItem> GetDeliveryItemTCB(int s, int t, string productid, string composition, string po, string customer,string bondingpo,string state);

        [OperationContract]
        int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo,string state);

        [OperationContract]
        void AddDeliveryItemTCB(DcDeliveryItem model);
        [OperationContract]
        void UpdateDeliveryItemTCB(DcDeliveryItem model);



        [OperationContract]
        List<DcDelivery> GetDelivery(int s, int t, string deliveryname);
        [OperationContract]
        int GetDeliveryCount(string deliveryname);


        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemTCBByDeliveryID(Guid id);

    }
}