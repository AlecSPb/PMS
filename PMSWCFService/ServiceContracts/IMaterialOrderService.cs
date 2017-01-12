using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IMaterialOrderService
    {
        [OperationContract]
        List<DcMaterialOrder> GetMaterialOrderBySearchInPage(int skip, int take, string orderPo, string supplier);

        [OperationContract]
        int AddMaterialOrder(DcMaterialOrder model);
        [OperationContract]
        int UpdateMaterialOrder(DcMaterialOrder model);
        [OperationContract]
        int DeleteMaterialOrder(Guid id);


        [OperationContract]
        List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id);

        [OperationContract]
        int AddMaterialOrderItem(DcMaterialOrderItem model);
        [OperationContract]
        int UpdateMaterialOrderItem(DcMaterialOrderItem model);
        [OperationContract]
        int DeleteMaterialOrderItem(Guid id);


    }
}