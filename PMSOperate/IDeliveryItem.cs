using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSIService
{
   public interface IDeliveryItem
    {
        IList<DeliveryItem> GetItemsByDeliveryID(Guid deliveryId);
        DeliveryItem GetItemByID(Guid deliveryItemId);
        int Add(DeliveryItem deliveryItem);
        int Update(DeliveryItem deliveryItem);
        int Disable(DeliveryItem deliveryItem);
    }
}
