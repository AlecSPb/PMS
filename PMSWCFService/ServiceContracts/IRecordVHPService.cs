using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordVHPService
    {
        [OperationContract]
        List<DcRecordVHP> GetRecordVHP(Guid planVHPId);
        [OperationContract]
        int GetRecordVHPCount();

        [OperationContract]
        int AddRecordVHP(DcRecordVHP model);
        [OperationContract]
        int UpdateReocrdVHP(DcRecordVHP model);

        [OperationContract]
        int AddRecordVHPByUID(DcRecordVHP model, string uid);
        [OperationContract]
        int UpdateRecordVHPByUID(DcRecordVHP model, string uid);
        [OperationContract]
        int DeleteRecordVHP(Guid id);
    }
}
