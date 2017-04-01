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
    public interface IMissonService
    {
        [OperationContract]
        List<DcOrder> GetMissons(int skip,int take);

        [OperationContract]
        List<DcPlanVHP> GetPlans(Guid id);

        [OperationContract]
        int GetMissonsCount();
    }
}
