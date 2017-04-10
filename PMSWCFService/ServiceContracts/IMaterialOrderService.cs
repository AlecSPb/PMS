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
        int GetMaterialOrderCountBySearch(string orderPo, string supplier);

        [OperationContract]
        List<DcMaterialOrder> GetMaterialOrderForSanjie(int skip, int take, string orderPo);
        [OperationContract]
        int GetMaterialOrderCountForSanjie(string orderPo);




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
        /// <summary>
        /// 用来给原料入库提供选择
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DcMaterialOrderItem> GetMaterialOrderItems(int skip, int take);
        [OperationContract]
        int GetMaterialOrderItemsCount();
    }
}