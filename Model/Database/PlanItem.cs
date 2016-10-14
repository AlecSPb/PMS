using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 计划项
    /// 属于Plan的子项，按照订单生成的
    /// 一个PlanItem对应模具当中的一块靶
    /// 相同成分和尺寸的也要分开成不同的PlanItem
    /// </summary>
    public class PlanItem
    {
        public Guid ID { get; set; }
        public Guid PlanID { get; set; }//对应的计划
        public Guid OrderID { get; set; }//对应的订单

        //制粉相关
        public double CalculationDensity { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        //单片装料重量按照经验往往比上面计算得出的理论重量要多
        public double PowderWeight { get; set; }
        public double GrainSize { get; set; }
        public string MillingRequirement { get; set; }

        //加工回收要求
        public string LaterProcess { get; set; }
        public string MachineRequirement { get; set; }//加工需求

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
