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
        //不再使用此方法，GetFailuresBySearch取代之
        [OperationContract]
        List<DcFailure> GetFailures(int s, int t, string stage);


        [OperationContract]
        List<DcFailure> GetFailuresBySearch(int s, int t, string productid, string composition, string stage);
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
