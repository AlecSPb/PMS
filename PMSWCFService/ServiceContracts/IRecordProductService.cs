using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordProductService
    {
        [OperationContract]
        List<DcRecordProduct> GetRecordProductBySearchInPage(int skip, int take, string productId, string compositionStd);
        [OperationContract]
        int GetRecordProductCountBySearchInPage(string productId, string compositionStd);
        [OperationContract]
        int AddRecordProduct(DcRecordProduct model);
        [OperationContract]
        int UpdateRecordProduct(DcRecordProduct model);
        [OperationContract]
        int DeleteRecordProduct(Guid id);
    }
}