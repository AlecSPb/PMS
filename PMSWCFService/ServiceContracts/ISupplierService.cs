using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSDAL;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ISupplierService
    {
        [OperationContract]
        int AddSupplier(DcBDSupplier model);
        [OperationContract]
        int UpdateSupplier(DcBDSupplier model);
        [OperationContract]
        int DeleteSupplier(DcBDSupplier model);

        [OperationContract]
        List<DcBDSupplier> GetSuppliers();

    }
}