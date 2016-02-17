using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class MainOrderServcie : IMainOrderService
    {
        private ProductionManagementModel db = new ProductionManagementModel();

        /// <summary>
        /// 获取所有订单列表
        /// </summary>
        /// <returns></returns>
        public List<V_MainOrder> GetAllMainOrders()
        {
            return db.V_MainOrder.OrderByDescending(o => o.OrderDate).ToList();
        }


    }
}
