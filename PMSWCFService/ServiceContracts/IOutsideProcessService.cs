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
    public interface IOutsideProcessService
    {
        [OperationContract]
        List<DcOutsideProcess> GetOutsideProcess(int s, int t, string productid, string composition);
        [OperationContract]
        int GetOutsideProcessCount(string productid, string composition);

        [OperationContract]
        List<DcOutsideProcess> GetOutsideProcessUnCompleted(int s, int t);
        [OperationContract]
        int GetOutsideProcessUnCompletedCount();

        [OperationContract]
        int Add(DcOutsideProcess model);
        [OperationContract]
        int Update(DcOutsideProcess model);
    }
}
