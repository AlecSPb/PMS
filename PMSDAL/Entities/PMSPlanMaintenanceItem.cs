using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Entities
{
    public class PMSPlanMaintenanceItem
    {
        public Guid ID { get; set; }
        public Guid MaintenancePlanID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string Content { get; set; }

    }
}
