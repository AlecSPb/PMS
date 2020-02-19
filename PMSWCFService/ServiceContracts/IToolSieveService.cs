using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IToolSieveService
    {
        [OperationContract]
        List<DcToolSieve> GetToolSieve();
        [OperationContract]
        int GetToolSieveCount();

        [OperationContract]
        void AddToolSieve(ToolSieve model);
        [OperationContract]
        void UpdateToolSieve(ToolSieve model);
    }
}
