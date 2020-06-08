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

        public static bool IsSeAsGeBondingUsingElastmer(Order currentOrder)
        {
            if (currentOrder == null) return true;
            bool is440or444 = currentOrder.Dimension.Contains("440") || currentOrder.Dimension.Contains("444.7");
            if (is440or444)
            {
                return currentOrder.BondingRequirement.Contains("Elastomer");
            }
            return true;
        }

    }
}
