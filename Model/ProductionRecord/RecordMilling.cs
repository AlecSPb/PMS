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
        //public TargetData TargetInformation { get; set; }

        //需要记录的信息
        public string RawMaterial { get; set; }
        public string FromWho { get; set; }
        public string ExtraInformation { get; set; }
        public string MillingTool{ get; set; }
        public string GasProtection { get; set; }
        public double MaterialIn { get; set; }
        public double MaterialOut { get; set; }
        public double MaterialRemain { get; set; }


        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
