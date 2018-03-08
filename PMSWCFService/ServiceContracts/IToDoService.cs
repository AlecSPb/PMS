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
        List<DcToDo> GetToDo(string title, int s, int t);

        [OperationContract]
        int GetToDoCount(string title);

        [OperationContract]
        int Add(DcToDo model);

        [OperationContract]
        int Update(DcToDo model);

        [OperationContract]
        int Delete(Guid id);
    }
}
