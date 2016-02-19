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


        [OperationContract]
        bool AddMainOrder(MainOrder order);
        [OperationContract]
        bool UpdateMainOrder(MainOrder order);
        [OperationContract]
        bool DeleteMainOrder(MainOrder order);

    }
}
