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
    public interface IRemainInventoryService
    {
        [OperationContract]
        List<DcRemainInventory> GetRemainInventories(string productid, string composition, int s, int t);
        [OperationContract]
        int GetRemainInventoryCounter(string productid, string composition);

        [OperationContract]
        int AddRemainInventory(DcRemainInventory model);
        [OperationContract]
        int UpdateRemainInventory(DcRemainInventory model);
    }
}
