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
    public interface IFeedBackService
    {
        [OperationContract]
        List<DcFeedBack> GetFeedBack(int s, int t, string productId, string composition, string customer);
        [OperationContract]
        int GetFeedBackCount(string productId, string composition, string customer);

        [OperationContract]
        int AddFeedBack(DcFeedBack model);
        [OperationContract]
        int UpdateFeedBack(DcFeedBack model);
        [OperationContract]
        int DeleteFeedBack(Guid id);
    }
}
