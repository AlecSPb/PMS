using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    public interface IRemainInventoryService
    {
        List<DcRemainInventory> GetRemainInventories(string productid, string composition, int s, int t);
        int GetRemainInventoryCounter(string productid, string composition);

        int AddRemainInventory(DcRemainInventory model);
        int UpdateRemainInventory(DcRemainInventory model);
    }
}
