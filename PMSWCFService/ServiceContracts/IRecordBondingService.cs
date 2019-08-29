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
        List<DcRecordBonding> GetRecordBondings(int skip, int take, string productid, string composition);
        [OperationContract]
        int GetRecordBondingCount(string productid, string composition);

        [OperationContract]
        List<DcRecordBonding> GetRecordBondingsNew(int skip, int take, string productid, string composition, string platelot);
        [OperationContract]
        int GetRecordBondingCountNew(string productid, string composition, string platelot);


        [OperationContract]
        int AddRecordBongding(DcRecordBonding model);
        [OperationContract]
        int UpdateRecordBongding(DcRecordBonding model);
        [OperationContract]
        int AddRecordBongdingByUID(DcRecordBonding model, string uid);
        [OperationContract]
        int UpdateRecordBongdingByUID(DcRecordBonding model, string uid);
        [OperationContract]
        int DeleteRecordBongding(Guid id);

        [OperationContract]
        List<DcRecordBonding> GetUnFinishedRecordBondings();

        [OperationContract]
        List<DcRecordBonding> GetRecordBondingByProductID(string productid);

        [OperationContract]
        int SetAllUnFinsihToTempFinish();

    }
}
