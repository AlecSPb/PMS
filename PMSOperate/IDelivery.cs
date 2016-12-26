using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSOperate
{
    public interface IDelivery
    {
        IList<Delivery> GetAll();
        IList<Delivery> GetBySearch();
        Delivery GetByID(Guid id);
        int Add(Delivery delivery);
        int Update(Delivery delivery);
        int Disable(Delivery delivery);

    }
}
