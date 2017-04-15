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
    public class RecordBonding
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get;  set; }
        public string State { get; set; }

        public string InstructionCode { get; set; }//操作手册代码

        //1.0靶材检查
        public string TargetLot { get; set; }
        public string TargetComposition { get; set; }
        public string TargetDimension { get; set; }
        public string TargetAppearance { get; set; }
        public string TargetWarpageCheck { get; set; }//翘曲检查结果
        public string TargetThicknessCheck { get; set; }//厚度检查结果
        public string TargetDiameterCheck { get; set; }//直径检查结果

        public string TargetCheckPerson { get; set; }//靶材检查人
        public DateTime TargetCheckTime { get; set; }//靶材检查日期

        //1.0背板检查
        public string PlateMaterial { get; set; }
        public string PlateLot { get; set; }//背板ID号
        public string PlateSerialNumber { get; set; }//序列号
        public string PlateBelong { get; set; }//背板归属
        public string PlateDimension { get; set; }
        public string PlateUseCount { get; set; }//使用次数
        public string PlateHardness { get; set; }//硬度
        public string PlateSuplier { get; set; }//供应商
        public string LastWeldMaterial { get; set; }//上次使用的焊接材料
        public string OtherRecord { get; set; }//其他记录
        public string PlateAppearance { get; set; }//外观情况
        //3.0背板前置处理
        public string PlateProcessRecord { get; set; }//前置处理结果检查记录

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
