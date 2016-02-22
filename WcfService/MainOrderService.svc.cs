using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccessLibrary;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MainOrderService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MainOrderService.svc 或 MainOrderService.svc.cs，然后开始调试。
    public class MainOrderService : IMainOrderService
    {
        private ProductionManagementModel dbcontext;
        public MainOrderService()
        {
            dbcontext = new ProductionManagementModel();
        }

        public bool AddMainOrder(MainOrder order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMainOrder(MainOrder order)
        {
            throw new NotImplementedException();
        }

        public List<MainOrder> GetAllMainOrders()
        {
            var query = from mo in dbcontext.MainOrders
                        select new MainOrder()
                        {
                            MainOrderId=mo.MainOrderId,
                            OrderDate=mo.OrderDate,
                            CustomerName=mo.CustomerName,
                            ProductName=mo.ProductName,
                            PO=mo.PO,
                            PMIWorkNumber=mo.PMIWorkNumber,
                            ProductType=mo.ProductType,
                            Purity=mo.Purity,
                            Shape=mo.Shape,
                            Dimension=mo.Dimension,
                            Quantity=mo.Quantity,
                            Unit=mo.Unit,
                            DeliveryDateExpect=mo.DeliveryDateExpect,
                            Consignee=mo.Consignee,
                            Priority=mo.Priority,
                            IsPlanFinished=mo.IsPlanFinished,
                            SampleRequirement=mo.SampleRequirement,
                            OrderState=mo.OrderState,
                            IsDeliveryFinished=mo.IsDeliveryFinished,
                            DeliveryDateFact=mo.DeliveryDateFact,
                            Remark=mo.Remark

                        };
            return query.ToList();
        }

        public MainOrder GetMainOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMainOrder(MainOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
