using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IMainOrderService”。
    [ServiceContract]
    public interface IMainOrderService
    {
        /// <summary>
        /// 返回全部的订单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MainOrder> GetAllMainOrders();
    }
}
