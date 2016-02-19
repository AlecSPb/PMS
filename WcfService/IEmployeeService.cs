using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IEmployeeService”。
    [ServiceContract]
    public interface IEmployeeService
    {
        /// <summary>
        /// 查询所有员工
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Employee> GetAllEmployees();
        /// <summary>
        /// 按照id返回员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Employee GetEmployeeById(Guid id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddEmployee(Employee employee);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEmployee(Employee employee);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteEmployee(Employee employee);

    }
}
