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
        List<DcDelivery> GetDeliveryBySearch(int skip, int take,string deliveryName);
        [OperationContract]
        int GetDeliveryCountBySearch(string deliveryName);



        [OperationContract]
        int AddDelivery(DcDelivery model);
        [OperationContract]
        int UpdateDelivery(DcDelivery model);
        [OperationContract]
        int DeleteDelivery(Guid id);

        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemByDeliveryID(Guid id);

        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItems(int skip ,int take,string productid,string composition);
        [OperationContract]
        int GetDeliveryItemsCount(string productid, string composition);


        [OperationContract]
        List<DcDeliveryItemExtra> GetDeliveryItemExtra(string productid, string composition);
        [OperationContract]
        int GetDeliveryItemExtraCount(string productid, string composition);







        [OperationContract]
        int AddDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int UpdateDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int DeleteDeliveryItem(Guid id);


    }
}
