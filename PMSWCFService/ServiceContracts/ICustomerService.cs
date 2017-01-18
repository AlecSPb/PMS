using PMSWCFService.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<DcBDCustomer> GetCustomer();
        [OperationContract]
        int AddCustomer(DcBDCustomer model);
        [OperationContract]
        int UpdateCustomer(DcBDCustomer model);
        [OperationContract]
        int DeleteCustomer(Guid id);
    }
}