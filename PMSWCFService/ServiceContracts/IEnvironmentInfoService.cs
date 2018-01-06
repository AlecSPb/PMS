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
    public interface IEnvironmentInfoService
    {
        [OperationContract]
        DcEnvironmentInfo GetEnvironmentInfo(string position);
        [OperationContract]
        int UpdateEnvironmentInfor(DcEnvironmentInfo data);
    }
}
