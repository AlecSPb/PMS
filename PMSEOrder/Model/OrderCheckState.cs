using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder.Model
{
    public class OrderCheckState
    {
        public bool ExistInPMS { get; set; }
        public Order CurrentOrder { get; set; }
    }
}
