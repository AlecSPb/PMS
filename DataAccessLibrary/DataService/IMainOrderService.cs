using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    /// <summary>
    /// 所有关于Order服务的实现
    /// </summary>
    public interface IMainOrderService
    {
        List<V_MainOrder> GetAllMainOrders();
        V_MainOrder GetOneMainOrderById(Guid id);
    }
}
