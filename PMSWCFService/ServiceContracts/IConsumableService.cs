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
    public interface IConsumableService
    {

        [OperationContract]
        void AddConsumablePurchase(DcConsumablePurchase model);

        [OperationContract]
        void UpdateConsumablePurchase(DcConsumablePurchase model);

        [OperationContract]
        List<DcConsumablePurchase> GetConsumablePurchase(int s, int t, string itemname);

        [OperationContract]
        int GetConsumablePurchaseCount(string item);



        [OperationContract]
        void AddConsumableInventory(DcConsumableInventory model);

        [OperationContract]
        void UpdateConsumableInventory(DcConsumableInventory model);

        [OperationContract]
        List<DcConsumableInventory> GetConsumableInventory(int s, int t, string itemname);

        [OperationContract]
        int GetConsumableInventoryCount(string item);


    }
}
