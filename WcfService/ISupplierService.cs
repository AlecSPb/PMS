using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISupplierService”。
    [ServiceContract]
    public interface ISupplierService
    {
        /// <summary>
        /// 得到所有的供应商
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Supplier> GetAllSuppliers();
        [OperationContract]
        bool AddSupplier(Supplier supplier);
        [OperationContract]
        bool UpdateSupplier(Supplier supplier);
        [OperationContract]
        bool DeleteSupplier(Supplier supplier);

    }
}
