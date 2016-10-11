using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class PMSGroup
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string ExtraInformation { get; set; }
        public int CurrentState { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
