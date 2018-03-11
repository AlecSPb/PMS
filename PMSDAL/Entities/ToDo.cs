using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class ToDo
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string Status { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public string PersonInCharge { get; set; }

        [DefaultValue(0)]
        public int Progress { get; set; }

        public DateTime FinishTime { get; set; }
        public string Remark { get; set; }
    }
}
