using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSIService
{
    public interface IOrder
    {
        IList<MainOrder> GetAll();
        IList<MainOrder> GetOrdersBySearch();
        MainOrder GetOrderByID(Guid id);
        int Add(MainOrder order);
        int Update(MainOrder order);
        int Disable(Guid id);
    }
}
