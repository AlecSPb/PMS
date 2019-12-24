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
    public interface IMachineFixService
    {
        [OperationContract]
        List<DcMachineFix> GetMachineFixes(int s, int t, string fixtype, string devicename, string partname);
        [OperationContract]
        int GetMachineFixeCount( string fixtype, string devicename, string partname);


        [OperationContract]
        void AddMachineFix(DcMachineFix model);
        [OperationContract]
        void UpdateMachineFix(DcMachineFix model);
        [OperationContract]
        void DeleteMachineFix(Guid id);
    }
}
