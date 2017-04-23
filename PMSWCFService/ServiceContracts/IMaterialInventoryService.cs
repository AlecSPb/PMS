using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IMaterialInventoryService
    {
        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take);
        [OperationContract]
        int GetMaterialInventoryInCount();

        [OperationContract]
        List<DcMaterialInventoryIn> GetMaterialInventoryInsBySearch(int skip, int take, string supplier, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int GetMaterialInventoryInCountBySearch(string supplier, string composition, string batchnumber, string pminumber);

        [OperationContract]
        int AddMaterialInventoryIn(DcMaterialInventoryIn model);
        [OperationContract]
        int UpdateMaterialInventoryIn(DcMaterialInventoryIn model);
        [OperationContract]
        int AddMaterialInventoryInByUID(DcMaterialInventoryIn model,string uid);
        [OperationContract]
        int UpdateMaterialInventoryInByUID(DcMaterialInventoryIn model,string uid);
        [OperationContract]
        int DeleteMaterialInventoryIn(Guid id);


        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take);
        [OperationContract]
        int GetMaterialInventoryOutCount();
        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOutsBySearch(int skip, int take,string receiver,string composition,string batchnumber,string pminumber);
        [OperationContract]
        int GetMaterialInventoryOutCountBySearch(string receiver, string composition, string batchnumber, string pminumber);
        [OperationContract]
        int AddMaterialInventoryOut(DcMaterialInventoryOut model);
        [OperationContract]
        int UpdateMaterialInventoryOut(DcMaterialInventoryOut model);
        [OperationContract]
        int AddMaterialInventoryOutByUID(DcMaterialInventoryOut model,string uid);
        [OperationContract]
        int UpdateMaterialInventoryOutByUID(DcMaterialInventoryOut model,string uid);
        [OperationContract]
        int DeleteMaterialInventoryOut(Guid id);
    }
}
