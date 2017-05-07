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
        //Order
        [OperationContract]
        List<DcMaterialOrder> GetMaterialOrder(int skip, int take, string orderPo);
        [OperationContract]
        int GetMaterialOrderCount(string orderPo);
        [OperationContract]
        List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id);


        //Order Item List
        [OperationContract]
        List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtras(int skip, int take, string composition, string pminumber, string orderitemnumber);
        [OperationContract]
        int GetMaterialOrderItemExtrasCount(string composition, string pminumber, string orderitemnumber);
        
        //Inventory
        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryInsBySearch(int skip, int take, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int GetMaterialInventoryInCountBySearch(string composition, string batchnumber, string pminumber);

        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOutsBySearch(int skip, int take, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int GetMaterialInventoryOutCountBySearch(string composition, string batchnumber, string pminumber);


        //Debit
        [OperationContract]
        List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creaditor);
        [OperationContract]
        int GetItemDebitCount(string itemType, string itemName, string creaditor);
    }
}
