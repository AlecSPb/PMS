using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        List<DcOrder> GetAllOrderInPage(int skip, int take);
        [OperationContract]
        List<DcOrder> GetOrderBySearchInPage(int skip, int take, string customer, string compositionstd);
        [OperationContract]
        int GetOrderCountBySearch(string customer, string compositionstd);

        [OperationContract]
        List<DcOrder> GetOrderUnCompleted(int skip, int take, string customer, string compositionstd);
        [OperationContract]
        int GetOrderCountrUnCompleted(string customer, string compositionstd);




        [OperationContract]
        List<DcOrder> GetOrderByYear(int skip, int take, int year);
        [OperationContract]
        int GetOrderCountByYear(int year);

        [OperationContract]
        int AddOrder(DcOrder order);
        [OperationContract]
        int AddOrderByUID(DcOrder order,string uid);
        [OperationContract]
        int UpdateOrder(DcOrder order);
        [OperationContract]
        int UpdateOrderByUID(DcOrder order,string uid);
        [OperationContract]
        int DeleteOrder(Guid id);


        [OperationContract]
        bool CheckPMINumberExisit(string pminumber);
    }
}