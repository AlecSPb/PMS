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
    public interface IToDoService
    {
        [OperationContract]
        List<DcToDo> GetToDo(string title,string personInCharge, int s, int t);

        [OperationContract]
        int GetToDoCount(string title,string personInCharge);

        [OperationContract]
        int AddToDo(DcToDo model);

        [OperationContract]
        int UpdateToDo(DcToDo model);

        [OperationContract]
        int DeleteToDo(Guid id);
    }
}
