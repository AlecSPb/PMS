using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PMSDAL
{
    /// <summary>
    /// 靶材信息
    /// </summary>
    public class RecordBondingHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get;  set; }
        public string State { get; set; }

        [DefaultValue(1)]
        public int PlanBatchNumber { get; set; }

        public string InstructionCode { get; set; }//操作手册代码

        public double WeldingRate { get; set; }


        //1.0靶材入料检查
        //基本信息，从测试直接录入
        public string TargetProductID { get; set; }//显示
        public string TargetComposition { get; set; }//显示
        public string TargetAbbr { get; set; }
        public string TargetCustomer { get; set; }
        public string TargetPO { get; set; }
        public string TargetPMINumber { get; set; }
        public string TargetWeight { get; set; }//显示
        public string TargetDimension { get; set; }//显示
        public string TargetDimensionActual { get; set; }
        public string TargetDefects { get; set; }
        public string TargetDetailRecord { get; set; }//复杂的信息写在这里
        public string CoverPlateNumber { get; set; }
        public string PlateType { get; set; }
        //检测信息
        public string TargetAppearance { get; set; }
        public string TargetWarpageCheck { get; set; }//翘曲检查结果
        public string TargetThicknessCheck { get; set; }//厚度检查结果
        public string TargetDiameterCheck { get; set; }//直径检查结果

        public string TargetPerson { get; set; }//靶材检查人
        public DateTime TargetCheckTime { get; set; }//靶材检查日期

        //1.0背板入料检查
        public string PlateLot { get; set; }//背板编号
        public string PlateMaterial { get; set; }
        public string PlateBelong { get; set; }//背板归属
        public string PlateDimension { get; set; }
        public string PlateUseCount { get; set; }//使用次数
        public string PlateHardness { get; set; }//硬度
        public string PlateSuplier { get; set; }//供应商
        public string PlateLastWeldMaterial { get; set; }//上次使用的焊接材料
        public string PlateOtherRecord { get; set; }//其他记录
        public string PlateAppearance { get; set; }//外观情况

        public string PlatePerson { get; set; }//靶材检查人
        public DateTime PlateCheckTime { get; set; }//靶材检查日期


        //2.0靶材前置处理

        public string TargetPreProcessRecord { get; set; }
        public string TargetPreProcessPerson { get; set; }//靶材检查人
        public DateTime TargetPreProcessCheckTime { get; set; }//靶材检查日期

        //3.0背板前置处理
        public string PlatePreProcessRecord { get; set; }//前置处理结果检查记录
        public string PlatePreProcessPerson { get; set; }//靶材检查人
        public DateTime PlatePreProcessCheckTime { get; set; }//靶材检查日期

        //4.0接合
        public string WeldMaterial { get; set; }
        public string WeldCuStringDiameter { get; set; }
        public string WeldHold { get; set; }//负重情况
        public string WeldPerson { get; set; }//靶材检查人
        public DateTime WeldCheckTime { get; set; }//靶材检查日期

        //5.0翘曲修正
        public string WarpageFix { get; set; }
        public string WarpagePerson { get; set; }//靶材检查人
        public DateTime WarpageCheckTime { get; set; }//靶材检查日期




        //6.0尺寸检查
        public string DimensionCheck { get; set; }
        public string DimensionWarpageCheck { get; set; }

        public string DimensionPerson { get; set; }//靶材检查人
        public DateTime DimensionCheckTime { get; set; }//靶材检查日期



        //7.0结合率检查
        public string BindingCheck { get; set; }
        public string BindingPerson { get; set; }//靶材检查人
        public DateTime BindingCheckTime { get; set; }//靶材检查日期



        //8.0喷砂处理
        public string SprayCheck { get; set; }
        public string SprayPerson { get; set; }//靶材检查人
        public DateTime SprayCheckTime { get; set; }//靶材检查日期



        //9.0清洁检查
        public string CleanCheck { get; set; }
        public string CleanPerson { get; set; }//靶材检查人
        public DateTime CleanCheckTime { get; set; }//靶材检查日期




        //10 外观检查
        public string ApperanceCheck { get; set; }
        public string ApperancePerson { get; set; }//靶材检查人
        public DateTime ApperanceCheckTime { get; set; }//靶材检查日期

        //11真空包装
        public string PackCheck { get; set; }
        public string PackPerson { get; set; }//靶材检查人
        public DateTime PackCheckTime { get; set; }//靶材检查日期

        //总结
        public string Remark { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
