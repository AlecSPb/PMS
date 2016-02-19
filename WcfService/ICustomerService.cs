using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>
        /// 得到所有客户
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        bool AddCustomer(Customer customer);
        [OperationContract]
        bool UpdateCustomer(Customer customer);
        [OperationContract]
        bool DeleteCustomer(Customer customer);

    }
}
