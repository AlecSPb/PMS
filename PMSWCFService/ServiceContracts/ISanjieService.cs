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

        [OperationContract]
        List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtrasUnCompleted(int skip, int take, string composition, string pminumber, string orderitemnumber);
        [OperationContract]
        int GetMaterialOrderItemExtrasUnCompletedCount(string composition, string pminumber, string orderitemnumber);

        //Finish Order Item
        [OperationContract]
        int FinishMaterialOrderItem(Guid id,string uid);

        [OperationContract]
        int FinishMaterialOrder(Guid id, string uid);

        [OperationContract]
        int FinishMaterialOrderItemWithIngredient(Guid id, string uid,string ingredient);

        //Inventory
        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int GetMaterialInventoryInCount(string composition, string batchnumber, string pminumber);

        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int GetMaterialInventoryOutCount(string composition, string batchnumber, string pminumber);


        //Debit
        [OperationContract]
        List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName);
        [OperationContract]
        int GetItemDebitCount(string itemType, string itemName);


        //Output
        [OperationContract]
        List<DcMaterialOrderItemExtra> GetMaterialOrderItemExtraByYear(int skip, int take, int year);
        [OperationContract]
        int GetMaterialOrderItemExtraCountByYear(string composition, string pminumber, int year);
        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryInByYear(int skip, int take, int year);
        [OperationContract]
        int GetMaterialInventoryInCountByYear(int year);
        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOutsByYear(int skip, int take, int year);
        [OperationContract]
        int GetMaterialInventoryOutCountByYear(int year);
    }
}
