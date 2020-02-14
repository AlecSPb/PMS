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
        List<DcToolSieve> GetToolSieve();
        int GetToolSieveCount();

        void AddToolSieve(ToolSieve model);
        void UpdateToolSieve(ToolSieve model);
    }
}
