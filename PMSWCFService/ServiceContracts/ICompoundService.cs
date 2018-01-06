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
        List<DcBDCompound> GetCompound(int skip, int take, string searchComposition);
        [OperationContract]
        int GetCompoundCount(string searchComposition);


        [OperationContract]
        int AddCompound(DcBDCompound model);
        [OperationContract]
        int UpdateCompound(DcBDCompound model);
        [OperationContract]
        int DeleteCompound(Guid id);
        [OperationContract]
        bool IsCompoundExist(string materialName);
    }
}