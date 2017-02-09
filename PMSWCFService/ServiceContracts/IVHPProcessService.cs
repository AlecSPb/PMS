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
    public interface IVHPProcessService
    {
        [OperationContract]
        List<DcBDVHPProcess> GetVHPProcess();
        [OperationContract]
        int AddVHPProcess(DcBDVHPProcess model);
        [OperationContract]
        int UpdateVHPProcess(DcBDVHPProcess model);
        [OperationContract]
        int DeleteVHPProcess(Guid id);
    }
}
