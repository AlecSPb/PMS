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
        [OperationContract]
        List<DcFailure> GetFailures(int s, int t, string stage);
        [OperationContract]
        int GetFailuresCount(string stage);
        [OperationContract]
        int GetFailuresCountByProductID(string productid);

        [OperationContract]
        int AddFailure(DcFailure model);
        [OperationContract]
        int UpdateFailure(DcFailure model);//update and delete

    }
}
