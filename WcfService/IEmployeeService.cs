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
    public interface IEmployeeService
    {
        /// <summary>
        /// 查询所有员工
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Employee> GetAllEmployees();

        [OperationContract]
        Employee GetEmployeeById(Guid id);

        [OperationContract]
        bool AddEmployee(Employee employee);
        [OperationContract]
        bool UpdateEmployee(Employee employee);
        [OperationContract]
        bool DeleteEmployee(Employee employee);

    }
}
