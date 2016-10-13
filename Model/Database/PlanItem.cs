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
    /// </summary>
    public class PlanItem
    {
        public Guid ID { get; set; }
        public Guid PlanID { get; set; }//对应的计划
        public Guid OrderID { get; set; }//对应的订单

        public double CalculationDensity { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        //单片装料重量和所有装料重量可以通过对上面的项目计算出来


        public string MillingRequirement { get; set; }

        public string FillingRequirement { get; set; }

        //确定此压片回收，加工，还是保留等操作
        public string LaterProcess { get; set; }
        public string MachineRequirement { get; set; }//加工需求

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
