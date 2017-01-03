using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;
using System.Collections.ObjectModel;

namespace PMSTabletClient.Models
{
    public class ModelFactory
    {
        public static void FillOrder(ObservableCollection<MainOrder> mainOrder)
        {
            var orders = new List<MainOrder>();
            for (int i = 0; i < 10; i++)
            {
                orders.Add(new MainOrder()
                {
                    ID = Guid.NewGuid(),
                    CustomerName = "Midsummer "+i,
                    PO = "20123123123",
                    PMIWorkingNumber = "161112-A",
                    CompositionStandard = "Cu22.8In20Ga7Se50.2 " +i,
                    CompositionOriginal = "Cu22.8In20Ga7Se50.2+NaF",
                    CompositoinAbbr = "CIGS",
                    Purity = "99.999%",
                    Quantity = 12,
                    QuantityUnit = "Piece",
                    Dimension = "230 OD x 4mm",
                    DimensionRequirement = "normal",
                    SampleRequirement = "No",
                    ScheduleDeliveryDate = new DateTime(2016, 12, 10),
                    MinimumRequirement = "normal",
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

            orders.ForEach(o =>
            {
                mainOrder.Add(o);
            });

        }
    }
}
