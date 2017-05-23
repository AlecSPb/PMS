using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class PlanVHPConclusion
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public Guid PlanID { get; set; }//对应热压计划ID

        public int Grade { get; set; }
        public string Description { get; set; }

        public DateTime UpdateTime { get; set; }
        public string Updator { get; set; }
    }
}
