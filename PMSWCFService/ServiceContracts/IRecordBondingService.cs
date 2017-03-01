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
        List<DcRecordBonding> GetRecordBondings(int skip, int take);
        [OperationContract]
        int GetRecordBondingCount();
        [OperationContract]
        List<DcRecordBondingPlate> GetRecordBondingPlates(int skip, int take);

        [OperationContract]
        List<DcRecordBondingPlate> GetRecordBondingPlates(Guid bondingId);
        [OperationContract]
        List<DcRecordBondingTarget> GetRecordBondingTargets(Guid bongdingId);

        [OperationContract]
        int AddRecordBongding(DcRecordBonding model);
        [OperationContract]
        int UpdateRecordBongding(DcRecordBonding model);
        [OperationContract]
        int DeleteRecordBongding(Guid id);

        [OperationContract]
        int AddRecordBongdingTarget(DcRecordBondingTarget model);
        [OperationContract]
        int UpdateRecordBongdingTarget(DcRecordBondingTarget model);
        [OperationContract]
        int DeleteRecordBongdingTarget(Guid id);

        [OperationContract]
        int AddRecordBongdingPlate(DcRecordBondingPlate model);
        [OperationContract]
        int UpdateRecordBongdingPlate(DcRecordBondingPlate model);
        [OperationContract]
        int DeleteRecordBongdingPlate(Guid id);
    }
}
