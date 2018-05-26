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
    public interface IToolInventoryService
    {
        [OperationContract]
        IList<DcToolFilling> GetToolFillings(string elementA, string elementB);

        [OperationContract]
        int GetToolFillingsCount();

        [OperationContract]
        IList<DcToolMilling> GetToolMillings(string elementA, string elementB);
        [OperationContract]
        int GetToolMillingsCount();
    }
}
