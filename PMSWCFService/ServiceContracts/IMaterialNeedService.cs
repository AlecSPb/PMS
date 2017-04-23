using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IMaterialNeedService
    {
        [OperationContract]
        List<DcMaterialNeed> GetMaterialNeedBySearchInPage(int skip, int take, string composition);
        [OperationContract]
        int GetMaterialNeedCountBySearch(string composition);
        [OperationContract]
        int AddMaterialNeed(DcMaterialNeed model);
        [OperationContract]
        int UpdateMaterialNeed(DcMaterialNeed model);
        [OperationContract]
        int AddMaterialNeedByUID(DcMaterialNeed model,string uid);
        [OperationContract]
        int UpdateMaterialNeedByUID(DcMaterialNeed model,string uid);
        [OperationContract]
        int DeleteMaterialNeed(Guid id);
    }
}
