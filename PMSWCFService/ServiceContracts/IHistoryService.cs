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
    public interface IHistoryService
    {
        [OperationContract]
        int OrderHistoryCount();
        [OperationContract]
        int VHPPlanHistoryCount();
        [OperationContract]
        int MaterialNeedHistoryCount();
        [OperationContract]
        int MaterialOrderHistoryCount();
        [OperationContract]
        int MaterialOrfderItemHistoryCount();
        [OperationContract]
        int MaterialInventoryInHistoryCount();
        [OperationContract]
        int MaterialInventoryOutHistoryCount();
        [OperationContract]
        int RecordMillingHistoryCount();
        [OperationContract]
        int ReordVHPHistoryCount();
        [OperationContract]
        int RecordDeMoldHistoryCount();
        [OperationContract]
        int RecordMachineHistoryCount();
        [OperationContract]
        int RecordTestHistoryCount();
        [OperationContract]
        int RecordBondingHistoryCount();
        [OperationContract]
        int ProductHistoryCount();
        [OperationContract]
        int DeliveryHistoryCount();
        [OperationContract]
        int DeliveryItemHistoryCount();
    }
}
