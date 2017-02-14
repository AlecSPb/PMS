using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using System.ServiceModel;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRecordTestResultService
    {
        [OperationContract]
        List<DcRecordTestResult> GetRecordTestResultBySearchInPage(int skip, int take, string productId, string compositionStd);
        [OperationContract]
        int GetRecordTestResultCountBySearchInPage(string productId, string compositionStd);
        [OperationContract]
        int AddRecordTestResult(DcRecordTestResult model);
        [OperationContract]
        int UpdateRecordTestResult(DcRecordTestResult model);
        [OperationContract]
        int DeleteRecordTestResult(Guid id);
    }
}