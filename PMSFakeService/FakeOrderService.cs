using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;
using PMSIService;

namespace PMSFakeService
{
    public class FakeOrderService : IOrder
    {
        public int Add(MainOrder order)
        {
            throw new NotImplementedException();
        }

        public int Disable(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<MainOrder> GetAll()
        {
            var orders = new List<MainOrder>();
            for (int i = 0; i < 30; i++)
            {
                orders.Add(new MainOrder()
                {
                    ID = Guid.NewGuid(),
                    CustomerName = "Midsummer-" + i,
                    PO = "20123123123",
                    PMIWorkingNumber = "161112-A",
                    CompositionStandard = "Cu22.8In20Ga7Se50.2-" + i,
                    CompositionOriginal = "Cu22.8In20Ga7Se50.2+NaF",
                    CompositoinAbbr = "CIGS",
                    Purity = "99.999%",
                    Quantity = 12,
                    QuantityUnit = "Piece",
                    Dimension = "230 OD x 4mm",
                    DimensionNeed = "normal",
                    SampleNeed = "No",
                    ScheduleDeliveryDate = new DateTime(2016, 12, 10),
                    DefectAccept = "normal",
                    Priority = 1,
                    CurrentState = 1,
                    CurrentStateReason = "",
                    CreateTime = DateTime.Now,
                    Creator = "xs.zhou",
                    Reviewer = "xs.zhou2",
                    ReviewPassed = true,
                    ReviewDate = DateTime.Now,
                    PolicyType = "VHP",
                    PolicyContent = "",
                    PolicyMakeDate = DateTime.Now
                });
            }

            return orders;
        }

        public MainOrder GetOrderByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<MainOrder> GetOrdersBySearch()
        {
            throw new NotImplementedException();
        }

        public int Update(MainOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
