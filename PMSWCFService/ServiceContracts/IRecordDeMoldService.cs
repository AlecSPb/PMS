using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using PMSWCFService.DataContracts;


namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordDeMoldService
    {
        [OperationContract]
        List<DcRecordDeMold> GetRecordDeMolds(int skip, int take);
        [OperationContract]
        int GetRecordDeMoldsCount();

        [OperationContract]
        List<DcRecordDeMold> GetRecordDeMoldsByVHPPlanLot(int skip, int take, string vhpplanlot,string  composition);

        [OperationContract]
        int GetRecordDeMoldsCountByVHPPlanLot(string vhpplanlot, string composition);



        [OperationContract]
        int AddRecordDeMold(DcRecordDeMold model);
        [OperationContract]
        int UpdateRecordDeMold(DcRecordDeMold model);
        [OperationContract]
        int AddRecordDeMoldByUID(DcRecordDeMold model, string uid);
        [OperationContract]
        int UpdateRecordDeMoldByUID(DcRecordDeMold model, string uid);
        [OperationContract]
        int DeleteRecordDeMold(Guid id);

        //用于集成查询功能
        [OperationContract]
        List<DcRecordDeMold> GetRecordDeMoldsByPMINumber(string pminumber);
    }
}
