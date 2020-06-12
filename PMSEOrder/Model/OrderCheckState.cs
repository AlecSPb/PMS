using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder.Model
{
    public class OrderCheckState
    {
        public Guid GUIDID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string Composition { get; set; }
        public DateTime CreateTime { get; set; }
        public bool CheckState { get; set; }
    }
}
