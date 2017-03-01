using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 靶材信息
    /// </summary>
    [DataContract]
    public class DcRecordBondingTarget
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string CreateTime { get;  set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid BondingID { get; set; }//foreign key
        [DataMember]
        public Guid PlateID { get; set; }//使用的背板ID

        //1.0靶材检查
        [DataMember]
        public string TargetLot { get; set; }
        [DataMember]
        public string TargetComposition { get; set; }
        [DataMember]
        public string TargetDimension { get; set; }
        [DataMember]
        public string TargetAppearance { get; set; }
        [DataMember]
        public string TargetWarpageCheck { get; set; }//翘曲检查结果
        [DataMember]
        public string TargetThicknessCheck { get; set; }//厚度检查结果
        [DataMember]
        public string TargetDiameterCheck { get; set; }//直径检查结果


        //2.0靶材前置处理
        [DataMember]
        public string TargetProcessRecord { get; set; }
        //接合
        [DataMember]
        public string WeldMaterial { get; set; }
        [DataMember]
        public double CuStringDiameter { get; set; }
        //翘曲修正
        [DataMember]
        public string BondWarpageFix { get; set; }
        //尺寸检查
        [DataMember]
        public string BondDimensionCheck { get; set; }
        [DataMember]
        public string BondWarpageCheck { get; set; }
        //结合率检查
        [DataMember]
        public string BondCheck { get; set; }
        //喷砂处理
        [DataMember]
        public string BondCleanCheck { get; set; }
        //外观检查
        [DataMember]
        public string BondAppearanceCheck { get; set; }
    }
}
