using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PMSDAL
{

    /// <summary>
    /// 热压计划
    /// 计划是按照热压日期，热压设备和热压模具来唯一确定的
    /// 这个热压计划只用来指导一次热压活动的唯一依据
    /// 其余所有步骤，制粉，装模具，热压，取模，还有加工的依据和要求都来自于计划表
    /// </summary>
    public class PMSPlanVHPHistory
    {
        public Guid ID { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

        public Guid OrderID { get; set; }
        public string SearchCode { get; set; }
        public DateTime PlanDate { get; set; }//生产日期
        public string VHPDeviceCode { get; set; }//生产机器代码
        public int PlanLot { get; set; }//生产批次，区分同一天同一台的机器的不同批次，默认是1
        public string PlanType { get; set; }

        public string MoldType { get; set; }
        public double CalculationDensity { get; set; }
        public double MoldDiameter { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public double SingleWeight { get; set; }
        public double AllWeight { get; set; }
        public string GrainSize { get; set; }

        public double RoomTemperature { get; set; }
        public double RoomHumidity { get; set; }

        public double PreTemperature { get; set; }
        public double PrePressure { get; set; }

        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Vaccum { get; set; }
        public double KeepTempTime { get; set; }

        public string ProcessCode { get; set; }

        public string MillingRequirement { get; set; }
        public string FillingRequirement { get; set; }
        public string VHPRequirement { get; set; }
        public string MachineRequirement { get; set; }
        public string SpecialRequirement { get; set; }


        public string Remark { get; set; }

        public bool IsLocked { get; set; }


        public int Grade { get; set; }
        public string Conclusion { get; set; }

        public DateTime UpdateTime { get; set; }
        public string Updator { get; set; }
        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
