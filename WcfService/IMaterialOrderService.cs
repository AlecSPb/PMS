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
        [OperationContract]
        List<MaterialOrder> GetAllMaterialOrders();
        [OperationContract]
        List<MaterialOrderItem> GetMaterialOrderItemsByMOId(Guid id);
        [OperationContract]
        List<MaterialOrderItemReceived> GetMaterialOrdreItemReceivedByMOId(Guid id);


        [OperationContract]
        bool AddMaterialOrder(MaterialOrder order);
        [OperationContract]
        bool AddMaterialOrderItem(MaterialOrderItem item);
        [OperationContract]
        bool AddMaterialOrderItemReceived(MaterialOrderItemReceived item);


        [OperationContract]
        bool UpdateMaterialOrder(MaterialOrder order);
        [OperationContract]
        bool UpdateMaterialOrderItem(MaterialOrderItem item);
        [OperationContract]
        bool UpdateMaterialOrderItemReceivied(MaterialOrderItemReceived item);

        [OperationContract]
        bool DeleteMaterialOrder(MaterialOrder order);
        [OperationContract]
        bool DeleteMaterialOrderItem(MaterialOrderItem item);
        [OperationContract]
        bool DeleteMaterialOrderItemReceived(MaterialOrderItemReceived item);

    }
}
