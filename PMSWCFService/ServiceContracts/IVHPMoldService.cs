using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IVHPMoldService
    {
        [OperationContract]
        List<DcBDVHPMold> GetVHPMold();
        [OperationContract]
        int AddVHPMold(DcBDVHPMold model);
        [OperationContract]
        int UpdateVHPMold(DcBDVHPMold model);
        [OperationContract]
        int DeleteVHPMold(Guid id);
    }
}
