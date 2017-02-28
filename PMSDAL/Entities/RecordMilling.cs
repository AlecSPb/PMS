using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 制粉记录
    /// </summary>
    public class RecordMilling
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; set; }
        public string State { get; set; }
        public Guid PlanID { get; set; }//Foreign Key
        //需要记录的信息
        public string RawMaterial { get; set; }
        public string FromWho { get; set; }//MaterialSource
        public string ExtraInformation { get; set; }
        public string MillingTool { get; set; }
        public string GasProtection { get; set; }
        public double MaterialIn { get; set; }
        public double MaterialOut { get; set; }
        public double MaterialRemain { get; set; }


        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
