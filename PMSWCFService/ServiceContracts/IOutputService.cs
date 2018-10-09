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
    public interface IOutputService
    {
        [OperationContract]
        List<PMS230DataModel> GetAll230Data(int s,int t);

        [OperationContract]
        int GetAll230DataCount();
    }
}
