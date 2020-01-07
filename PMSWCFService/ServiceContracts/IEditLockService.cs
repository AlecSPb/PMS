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
    public interface IEditLockService
    {
        [OperationContract]
        void Lock(DcEditLock model);
        [OperationContract]
        void UnLock(Guid id);
        [OperationContract]
        void UnLockAll();

        [OperationContract]
        DcEditLock CheckLock(string fingerprint);
    }
}
