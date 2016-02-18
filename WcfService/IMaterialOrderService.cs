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
    public interface IMaterialOrderService
    {
        /// <summary>
        /// 获取所有材料订单信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MaterialOrder> GetAllMaterialOrders();
        /// <summary>
        /// 按照材料订单Id查找所有订单项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<MaterialOrderItem> GetMaterialOrderItemsByMOId(Guid id);
        /// <summary>
        /// 按照材料订单Id查找所有材料接受单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<MaterialOrderItemReceived> GetMaterialOrdreItemReceivedByMOId(Guid id);


        /// <summary>
        /// 添加材料订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMaterialOrder(MaterialOrder order);
        /// <summary>
        /// 添加材料订单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMaterialOrderItem(MaterialOrderItem item);
        /// <summary>
        /// 添加材料接受单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMaterialOrderItemReceived(MaterialOrderItemReceived item);

        /// <summary>
        /// 更新材料订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMaterialOrder(MaterialOrder order);
        /// <summary>
        /// 更新材料订单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMaterialOrderItem(MaterialOrderItem item);
        /// <summary>
        /// 更新材料接受单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMaterialOrderItemReceivied(MaterialOrderItemReceived item);

        /// <summary>
        /// 删除材料订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMaterialOrder(MaterialOrder order);
        /// <summary>
        /// 删除材料订单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMaterialOrderItem(MaterialOrderItem item);
        /// <summary>
        /// 删除材料接受单项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMaterialOrderItemReceived(MaterialOrderItemReceived item);

    }
}
