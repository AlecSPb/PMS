using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSModel
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class PMSAccess
    {
        public Guid ID { get; set; }
        public string AccessName { get; set; }
        public int CurrentState { get; set; }
        public string ExtraInformation { get; set; }
    }
}
