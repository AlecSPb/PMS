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
    public interface IPlanVHPConclusionService
    {
        [OperationContract]
        List<DcPlanVHPConclusionExtra> GetPlanVHPConclusionExtra(int s, int t, string composition, string pminumber);
        [OperationContract]
        int GetPlanVHPConclusionExtraCount(string composition, string pminumber);


        [OperationContract]
        int AddPlanVHPConclusion(DcPlanVHPConclusion model);
        [OperationContract]
        int UpdatePlanVHPConclusion(DcPlanVHPConclusion model);
        [OperationContract]
        int DeletePlanVHPConclusion(Guid id);
    }
}
