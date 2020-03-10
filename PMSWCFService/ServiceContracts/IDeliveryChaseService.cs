using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IDeliveryChaseService
    {
        [OperationContract]
        List<DcDeliveryChase> GetDeliveryChaseAll(int s, int t, string lot, string composition);

        [OperationContract]
        int GetDeliveryChaseAllCount(string lot, string composition);


        [OperationContract]
        List<DcDeliveryChase> GetDeliveryChase(int s, int t, string lot, string composition);

        [OperationContract]
        int GetDeliveryChaseCount(string lot, string composition);

        [OperationContract]
        void AddDeliveryChaseCount(DcDeliveryChase model);
        [OperationContract]
        void UpdateDeliveryChaseCount(DcDeliveryChase model);
    }
}
