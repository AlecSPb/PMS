using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 绑定部分
    /// </summary>
    [DataContract]
    public class DcRecordBonding
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }

        //ExtraInformation
        [DataMember]
        public string InstructionCode { get; set; }//操作手册代码
        [DataMember]
        public string Remark { get; set; }


        //4.0结合过程-公共部分
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public string HeatRecord { get; set; }


    }
}
