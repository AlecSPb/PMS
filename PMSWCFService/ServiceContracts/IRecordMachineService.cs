using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordMachineService
    {
        [OperationContract]
        List<DcRecordMachine> GetRecordMachines(int skip, int take);
        [OperationContract]
        int GetRecordMachineCount();

        [OperationContract]
        List<DcRecordMachine> GetRecordMachinesByVHPPlanLot(int skip, int take, string vhpplanlot);
        [OperationContract]
        int GetRecordMachineCountByVHPPlanLot(string vhpplanlot);



        [OperationContract]
        int AddRecordMachine(DcRecordMachine model);
        [OperationContract]
        int UpdateRecordMachine(DcRecordMachine model);
        [OperationContract]
        int AddRecordMachineByUID(DcRecordMachine model, string uid);
        [OperationContract]
        int UpdateRecordMachineByUID(DcRecordMachine model, string uid);
        [OperationContract]
        int DeleteRecordMachine(Guid id);


        //用于集成查询功能
        [OperationContract]
        List<DcRecordMachine> GetRecordMachinesByPMINumber(string pminumber);
    }
}