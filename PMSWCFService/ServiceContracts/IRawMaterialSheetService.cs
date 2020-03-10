using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IRawMaterialSheetService
    {
        [OperationContract]
        List<DcRawMaterialSheet> GetRawMaterialSheet(int s, int t, string lot, string composition);

        [OperationContract]
        int GetRawMaterialSheetCount(string lot, string composition);

        [OperationContract]
        void AddRawMaterialSheet(DcRawMaterialSheet model);
        [OperationContract]
        void UpdateRawMaterialSheet(DcRawMaterialSheet model);
    }
}