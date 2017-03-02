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
        List<DcRecordDeMold> GetRecordDeMold(int skip, int take);
        [OperationContract]
        int AddRecordDeMold(DcRecordDeMold model);
        [OperationContract]
        int UpdateRecordDeMold(DcRecordDeMold model);
        [OperationContract]
        int DeleteRecordDeMold(Guid id);
    }
}
