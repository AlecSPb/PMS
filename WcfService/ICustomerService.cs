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

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddCustomer(Customer customer);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomer(Customer customer);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteCustomer(Customer customer);

    }
}
