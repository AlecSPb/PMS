using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ICompoundService
    {
        [OperationContract]
        List<DcBDCompound> GetAllCompounds();
        [OperationContract]
        int AddCompound(DcBDCompound model);
        [OperationContract]
        int UpdateCompound(DcBDCompound model);
        [OperationContract]
        int DeleteCompound(DcBDCompound model);
    }
}