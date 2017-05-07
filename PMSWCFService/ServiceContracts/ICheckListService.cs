using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ICheckListService
    {
        [OperationContract]
        List<DcCheckList> GetCheckList(int s, int t, string title);
        [OperationContract]
        int GetCheckListCount(string title);

        [OperationContract]
        int AddCheckList(DcCheckList model,string uid);
        [OperationContract]
        int UpdateCheckList(DcCheckList model,string uid);
        [OperationContract]
        int DeleteCheckList(Guid id,string uid);
    }
}
