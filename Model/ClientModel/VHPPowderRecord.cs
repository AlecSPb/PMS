using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ClientModel
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    public class VHPPowderRecord
    {
        public Guid ID { get; set; }
        public TargetData TargetInformation { get; set; }

    }
}
