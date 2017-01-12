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
        List<DcOrder> GetAllOrderInPage(int skip, int take, int state);
        [OperationContract]
        List<DcOrder> GetOrderBySearchInPage(int skip, int take, int state, string customer, string compositionstd);
        [OperationContract]
        int GetOrderCountBySearch(int state,string customer, string compositionstd);
        [OperationContract]
        int AddOrder(DcOrder order);
        [OperationContract]
        int UpdateOrder(DcOrder order);
        [OperationContract]
        int DeleteOrder(Guid id);
    }
}