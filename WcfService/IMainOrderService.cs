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
        /// 返回所有的订单列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MainOrder> GetAllMainOrders();
        /// <summary>
        /// 按照Id查找订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        MainOrder GetMainOrderById(Guid id);

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMainOrder(MainOrder order);
        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMainOrder(MainOrder order);
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMainOrder(MainOrder order);

    }
}
