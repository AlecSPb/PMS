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
    public interface IFailureService
    {
        List<DcFailure> GetFailures(int s, int t, string stage);
        int GetFailuresCount(string stage);


        int AddFailure(DcFailure model);
        int UpdateFailure(DcFailure model);//update and delete

    }
}
