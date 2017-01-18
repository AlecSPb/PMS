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
    public interface IVHPDeviceService
    {
        [OperationContract]
        List<DcBDVHPDevice> GetVHPDevice();
        [OperationContract]
        int AddVHPDevice(DcBDVHPDevice model);
        [OperationContract]
        int UpdateVHPDevice(DcBDVHPDevice model);
        [OperationContract]
        int DeleteVHPDevice(Guid id);
    }
}
