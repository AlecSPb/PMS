using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
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
        [DataMember]
        public int PlanBatchNumber { get; set; }
        [DataMember]
        public string CoverPlateNumber { get; set; }

        [DataMember]
        public string InstructionCode { get; set; }//操作手册代码
        [DataMember]
        public string PlateType { get; set; }
        //1.0靶材入料检查
        //基本信息，从测试直接录入
        [DataMember]
        public string TargetProductID { get; set; }//显示
        [DataMember]
        public string TargetComposition { get; set; }//显示
        [DataMember]
        public string TargetAbbr { get; set; }
        [DataMember]
        public string TargetCustomer { get; set; }
        [DataMember]
        public string TargetPO { get; set; }
        [DataMember]
        public string TargetPMINumber { get; set; }
        [DataMember]
        public string TargetWeight { get; set; }//显示
        [DataMember]
        public string TargetDimension { get; set; }//显示
        [DataMember]
        public string TargetDimensionActual { get; set; }
        [DataMember]
        public string TargetDefects { get; set; }
        [DataMember]
        public string TargetDetailRecord { get; set; }//复杂的信息写在这里

        //检测信息
        [DataMember]
        public string TargetAppearance { get; set; }
        [DataMember]
        public string TargetWarpageCheck { get; set; }//翘曲检查结果
        [DataMember]
        public string TargetThicknessCheck { get; set; }//厚度检查结果
        [DataMember]
        public string TargetDiameterCheck { get; set; }//直径检查结果
        [DataMember]
        public string TargetPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime TargetCheckTime { get; set; }//靶材检查日期

        //1.0背板入料检查
        [DataMember]
        public string PlateLot { get; set; }//背板编号
        [DataMember]
        public string PlateMaterial { get; set; }
        [DataMember]
        public string PlateBelong { get; set; }//背板归属
        [DataMember]
        public string PlateDimension { get; set; }
        [DataMember]
        public string PlateUseCount { get; set; }//使用次数
        [DataMember]
        public string PlateHardness { get; set; }//硬度
        [DataMember]
        public string PlateSuplier { get; set; }//供应商
        [DataMember]
        public string PlateLastWeldMaterial { get; set; }//上次使用的焊接材料
        [DataMember]
        public string PlateOtherRecord { get; set; }//其他记录
        [DataMember]
        public string PlateAppearance { get; set; }//外观情况
        [DataMember]
        public string PlatePerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime PlateCheckTime { get; set; }//靶材检查日期


        //2.0靶材前置处理
        [DataMember]
        public string TargetPreProcessRecord { get; set; }
        [DataMember]
        public string TargetPreProcessPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime TargetPreProcessCheckTime { get; set; }//靶材检查日期

        //3.0背板前置处理
        [DataMember]
        public string PlatePreProcessRecord { get; set; }//前置处理结果检查记录
        [DataMember]
        public string PlatePreProcessPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime PlatePreProcessCheckTime { get; set; }//靶材检查日期

        //4.0接合
        [DataMember]
        public string WeldMaterial { get; set; }
        [DataMember]
        public string WeldCuStringDiameter { get; set; }
        [DataMember]
        public string WeldHold { get; set; }//负重情况
        [DataMember]
        public string WeldPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime WeldCheckTime { get; set; }//靶材检查日期

        //5.0翘曲修正
        [DataMember]
        public string WarpageFix { get; set; }
        [DataMember]
        public string WarpagePerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime WarpageCheckTime { get; set; }//靶材检查日期




        //6.0尺寸检查
        [DataMember]
        public string DimensionCheck { get; set; }
        [DataMember]
        public string DimensionWarpageCheck { get; set; }

        [DataMember]
        public string DimensionPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime DimensionCheckTime { get; set; }//靶材检查日期



        //7.0结合率检查
        [DataMember]
        public string BindingCheck { get; set; }
        [DataMember]
        public string BindingPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime BindingCheckTime { get; set; }//靶材检查日期



        //8.0喷砂处理
        [DataMember]
        public string SprayCheck { get; set; }
        [DataMember]
        public string SprayPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime SprayCheckTime { get; set; }//靶材检查日期



        //9.0清洁检查
        [DataMember]
        public string CleanCheck { get; set; }
        [DataMember]
        public string CleanPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime CleanCheckTime { get; set; }//靶材检查日期




        //10 外观检查
        [DataMember]
        public string ApperanceCheck { get; set; }
        [DataMember]
        public string ApperancePerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime ApperanceCheckTime { get; set; }//靶材检查日期

        //11真空包装
        [DataMember]
        public string PackCheck { get; set; }
        [DataMember]
        public string PackPerson { get; set; }//靶材检查人
        [DataMember]
        public DateTime PackCheckTime { get; set; }//靶材检查日期

        [DataMember]
        public string Remark { get; set; }
    }
}