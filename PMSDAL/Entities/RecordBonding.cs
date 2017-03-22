using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 绑定部分
    /// </summary>
    public class RecordBonding
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        //ExtraInformation
        public string InstructionCode { get; set; }//操作手册代码
        public string Remark { get; set; }


        //4.0结合过程-公共部分
        public double Temperature { get; set; }
        public string HeatRecord { get; set; }


    }
}
