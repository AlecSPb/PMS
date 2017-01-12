using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSDAL
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class PMSRole
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string ExtraInformation { get; set; }
        public int State { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual List<PMSAccess> PMSAccesses { get; set; }
    }
}
