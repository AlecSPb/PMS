using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 外协订单
    /// </summary>
    public class SubContract
    {
        public Guid ID { get; set; }
        public string Supplier { get; set; }
        public string SubContractContent { get; set; }
        public string ExtraInformation { get; set; }

        public DateTime ScheduleDeliveryDate { get; set; }
        public DateTime ActualDeliveryDate { get; set; }

        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
