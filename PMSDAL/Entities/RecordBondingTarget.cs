using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 靶材信息
    /// </summary>
    public class RecordBondingTarget
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get;  set; }
        public string State { get; set; }

        public Guid BondingID { get; set; }//foreign key
        public Guid PlateID { get; set; }//使用的背板ID

        //1.0靶材检查
        public string TargetLot { get; set; }
        public string TargetComposition { get; set; }
        public string TargetDimension { get; set; }
        public string TargetAppearance { get; set; }
        public string TargetWarpageCheck { get; set; }//翘曲检查结果
        public string TargetThicknessCheck { get; set; }//厚度检查结果
        public string TargetDiameterCheck { get; set; }//直径检查结果


        //2.0靶材前置处理
        public string TargetProcessRecord { get; set; }
        //接合
        public string WeldMaterial { get; set; }
        public double CuStringDiameter { get; set; }
        //翘曲修正
        public string BondWarpageFix { get; set; }
        //尺寸检查
        public string BondDimensionCheck { get; set; }
        public string BondWarpageCheck { get; set; }
        //结合率检查
        public string BondCheck { get; set; }
        //喷砂处理
        public string BondCleanCheck { get; set; }
        //外观检查
        public string BondAppearanceCheck { get; set; }
    }
}
