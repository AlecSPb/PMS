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
    public interface IAnlysisService
    {
        [OperationContract]
        List<DcPlanTrace> GetPlanTrace(int s, int t, string searchCode, string composition, string pminumber);
        [OperationContract]
        int GetPlanTraceCount(string searchCode, string composition, string pminumber);

    }
}
