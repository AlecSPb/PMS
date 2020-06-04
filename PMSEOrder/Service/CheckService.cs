using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEOrder.Model;

namespace PMSEOrder.Service
{
    public class CheckService
    {
        public static bool IsBasicItemNotEmpty(Order currentorder)
        {
            if (currentorder == null) return false;
            bool result = !string.IsNullOrEmpty(currentorder.CustomerName)
                && !string.IsNullOrEmpty(currentorder.Composition)
                && !string.IsNullOrEmpty(currentorder.PO)
                && !string.IsNullOrEmpty(currentorder.Dimension);
            return result;
        }

        public static bool IsPONotRepeat(Order neworder)
        {
            var data = new DataService().GetAllOrder();
            var query = from o in data
                        where o.PO == neworder.PO
                        select o;
            return query.Count() == 0;
        }



    }
}
