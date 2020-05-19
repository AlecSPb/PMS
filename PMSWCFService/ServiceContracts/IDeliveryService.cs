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
        List<DcDelivery> GetDeliveryBySearch(int skip, int take, string deliveryName);
        [OperationContract]
        int GetDeliveryCountBySearch(string deliveryName);



        [OperationContract]
        int AddDelivery(DcDelivery model);
        [OperationContract]
        int UpdateDelivery(DcDelivery model);
        [OperationContract]
        int AddDeliveryByUID(DcDelivery model, string uid);
        [OperationContract]
        int UpdateDeliveryByUID(DcDelivery model, string uid);

        [OperationContract]
        int DeleteDelivery(Guid id);

        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemByDeliveryID(Guid id);

        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItems(int skip, int take, string productid, string composition);
        [OperationContract]
        int GetDeliveryItemsCount(string productid, string composition);


        [OperationContract]
        List<DcDeliveryItemExtra> GetDeliveryItemExtra(int skip, int take, string productid, string composition, string customer);
        [OperationContract]
        int GetDeliveryItemExtraCount(string productid, string composition, string customer);

        [OperationContract]
        List<DcDeliveryItemExtra> GetDeliveryItemExtraByYear(int skip, int take, int year);
        [OperationContract]
        int GetDeliveryItemExtraCountByYear(string productid, int year);

        [OperationContract]
        int AddDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int UpdateDeliveryItem(DcDeliveryItem model);
        [OperationContract]
        int AddDeliveryItemByUID(DcDeliveryItem model, string uid);
        [OperationContract]
        int UpdateDeliveryItemByUID(DcDeliveryItem model, string uid);
        [OperationContract]
        int DeleteDeliveryItem(Guid id);


        [OperationContract]
        List<DcDeliveryItem> GetDeliveryItemByProductID(string productid);

        [OperationContract]
        List<DcDelivery> GetDeliveryUnFinished();


        [OperationContract]
        bool CheckDeliveryItemExistByProductID(Guid id, string productid);


        [OperationContract]
        DateTime GetDeliveryLastUpdateTime(Guid id);

    }
}
