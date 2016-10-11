using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Group 和 Access的多对多关系表
    /// </summary>
    public class PMSGroupAcess
    {
        public Guid ID { get; set; }
        public Guid GroupID { get; set; }
        public Guid AccessID { get; set; }
    }
}
