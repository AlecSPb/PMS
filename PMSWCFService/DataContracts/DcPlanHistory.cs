using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{

    /// <summary>
    /// 热压计划
    /// 计划是按照热压日期，热压设备和热压模具来唯一确定的
    /// 这个热压计划只用来指导一次热压活动的唯一依据
    /// 其余所有步骤，制粉，装模具，热压，取模，还有加工的依据和要求都来自于计划表
    /// </summary>
    [DataContract]
    public class DcPlanHistory
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public Guid OrderID { get; set; }
        [DataMember]
        public string SearchCode { get; set; }
        [DataMember]
        public DateTime PlanDate { get; set; }//生产日期
        [DataMember]
        public string VHPDeviceCode { get; set; }//生产机器代码
        [DataMember]
        public int PlanLot { get; set; }//生产批次，区分同一天同一台的机器的不同批次，默认是1
        [DataMember]
        public string PlanType { get; set; }
        [DataMember]
        public string MoldType { get; set; }
        [DataMember]
        public double CalculationDensity { get; set; }
        [DataMember]
        public double MoldDiameter { get; set; }
        [DataMember]
        public double Thickness { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public double SingleWeight { get; set; }
        [DataMember]
        public double AllWeight { get; set; }
        [DataMember]
        public string GrainSize { get; set; }
        [DataMember]
        public double RoomTemperature { get; set; }
        [DataMember]
        public double RoomHumidity { get; set; }
        [DataMember]
        public double PreTemperature { get; set; }
        [DataMember]
        public double PrePressure { get; set; }
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public double Pressure { get; set; }
        [DataMember]
        public double Vaccum { get; set; }
        [DataMember]
        public double KeepTempTime { get; set; }
        [DataMember]
        public string ProcessCode { get; set; }
        [DataMember]
        public string MillingRequirement { get; set; }
        [DataMember]
        public string FillingRequirement { get; set; }
        [DataMember]
        public string VHPRequirement { get; set; }
        [DataMember]
        public string MachineRequirement { get; set; }
        [DataMember]
        public string SpecialRequirement { get; set; }

        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public int Grade { get; set; }
        [DataMember]
        public string Conclusion { get; set; }
        [DataMember]
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public string Updator { get; set; }
        //操作者和操作时间
        [DataMember]
        public Guid HistoryID { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public DateTime OperateTime { get; set; }
    }
}
