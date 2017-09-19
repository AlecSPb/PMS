using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class TaskPlan
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PersonCharge { get; set; }
        public DateTime DeadLine { get; set; }
        public string Remark { get; set; }

    }
}
