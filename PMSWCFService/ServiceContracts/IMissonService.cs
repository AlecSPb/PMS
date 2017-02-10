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
    public interface IMissonService
    {
        [OperationContract]
        List<DcOrder> GetMissonBySearchInPage(int skip,int take);
        [OperationContract]
        int GetMissonCountBySearch();
    }
}
