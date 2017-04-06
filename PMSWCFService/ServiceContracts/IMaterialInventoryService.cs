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
        int AddMaterialInventoryIn(DcMaterialInventoryIn model);
        [OperationContract]
        int UpdateMaterialInventoryIn(DcMaterialInventoryIn model);
        [OperationContract]
        int DeleteMaterialInventoryIn(Guid id);


        [OperationContract]
        List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take);
        [OperationContract]
        int AddMaterialInventoryOut(DcMaterialInventoryOut model);
        [OperationContract]
        int UpdateMaterialInventoryOut(DcMaterialInventoryOut model);
        [OperationContract]
        int DeleteMaterialInventoryOut(Guid id);
    }
}
