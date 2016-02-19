using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“IMaterialOrderService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 IMaterialOrderService.svc 或 IMaterialOrderService.svc.cs，然后开始调试。
    public class MaterialOrderService : IMaterialOrderService
    {
        public bool AddMaterialOrder(MaterialOrder order)
        {
            throw new NotImplementedException();
        }

        public bool AddMaterialOrderItem(MaterialOrderItem item)
        {
            throw new NotImplementedException();
        }

        public bool AddMaterialOrderItemReceived(MaterialOrderItemReceived item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMaterialOrder(MaterialOrder order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMaterialOrderItem(MaterialOrderItem item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMaterialOrderItemReceived(MaterialOrderItemReceived item)
        {
            throw new NotImplementedException();
        }

        public List<MaterialOrder> GetAllMaterialOrders()
        {
            throw new NotImplementedException();
        }

        public List<MaterialOrderItem> GetMaterialOrderItemsByMOId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MaterialOrderItemReceived> GetMaterialOrdreItemReceivedByMOId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMaterialOrder(MaterialOrder order)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMaterialOrderItem(MaterialOrderItem item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMaterialOrderItemReceivied(MaterialOrderItemReceived item)
        {
            throw new NotImplementedException();
        }
    }
}
