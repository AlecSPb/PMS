using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 背板记录信息
    /// </summary>
    [DataContract]
    public class DcRecordBondingPlate
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get;  set; }
        [DataMember]
        public string State { get; set; }

        //1.0背板检查
        [DataMember]
        public string PlateMaterial { get; set; }
        [DataMember]
        public string PlateLot { get; set; }
        [DataMember]
        public string PlateDimension { get; set; }
        [DataMember]
        public string PlateUseCount { get; set; }//使用次数
        [DataMember]
        public string PlateHardness { get; set; }//硬度
        [DataMember]
        public string PlateSuplier { get; set; }//供应商
        [DataMember]
        public string LastWeldMaterial { get; set; }//上次使用的焊接材料
        [DataMember]
        public string OtherRecord { get; set; }//其他记录
        [DataMember]
        public string PlateAppearance { get; set; }//外观情况
        //3.0背板前置处理
        [DataMember]
        public string PlateProcessRecord { get; set; }//前置处理结果检查记录
    }
}
