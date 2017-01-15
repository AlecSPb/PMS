using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    public interface ICompoundService
    {
        List<DcCompound> GetAllCompounds();
        int AddCompound(DcCompound model);
        int UpdateCompound(DcCompound model);
        int DeleteCompound(DcCompound model);
    }
}