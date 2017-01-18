using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    public interface IRecordProductService
    {
        List<DcRecordProduct> GetRecordProductBySearchInPage(int skip, int take, string productId, string compositionStd);
        int GetRecordProductCountBySearchInPage(int skip, int take, string productid, string compositionStd);
        int AddRecordProduct(DcRecordProduct model);
        int UpdateRecordProduct(DcRecordProduct model);
        int DeleteRecordProduct(DcRecordProduct model);
    }
}