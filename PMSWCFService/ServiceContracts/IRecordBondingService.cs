using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using PMSWCFService.DataContracts;
using PMSDAL;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordBondingService
    {
        [OperationContract]
        List<DcRecordBonding> GetRecordBondings(int skip, int take,string TargetLot,string PlateLot);
        [OperationContract]
        int GetRecordBondingCount(string TargetLot, string PlateLot);




        [OperationContract]
        int AddRecordBongding(DcRecordBonding model);
        [OperationContract]
        int UpdateRecordBongding(DcRecordBonding model);
        [OperationContract]
        int DeleteRecordBongding(Guid id);


    }
}
