using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 送货单项目
    /// </summary>
    public class DeliveryItem
    {
        public Guid Id { get; set; }
        public string Lot { get; set; }
        public string Composition { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Size { get; set; }
        public string SpeicialRequirement { get; set; }
        public string CurrentState { get; set; }
    }
}
