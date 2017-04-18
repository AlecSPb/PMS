using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ISanjieService
    {
        [OperationContract]
        List<DcMaterialOrder> GetMaterialOrder(int skip, int take, string orderPo);
        [OperationContract]
        int GetMaterialOrderCount(string orderPo);

        [OperationContract]
        List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id);
        [OperationContract]
        int GetMaterialOrderItemCountByMaterialID(Guid id);


        [OperationContract]
        List<DcMaterialOrderItem> GetMaterialOrderItem(int skip, int take, string orderPo);
        [OperationContract]
        int GetMaterialOrderItemCount(string orderPo);



        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryIn(int skip, int take, string orderlot, string composition);

        [OperationContract]
        int GetMaterialInventoryInCount(int skip, int take, string orderlot, string composition);


        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOut(int skip, int take, string orderlot, string composition);

        [OperationContract]
        int GetMaterialInventoryOutCount(int skip, int take, string orderlot, string composition);
    }
}
