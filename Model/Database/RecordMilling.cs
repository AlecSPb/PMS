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
    public class RecordMilling
    {
        public Guid ID { get; set; }
        //靶材信息
        public TargetData TargetInformation { get; set; }

    }
}
