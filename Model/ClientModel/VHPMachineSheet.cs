using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 机械加工记录
    /// </summary>
    public class VHPMachineSheet
    {
        public Guid ID { get; set; }
        public TargetData CurrentTarget { get; set; }
        //加工要求
        public string Dimension{ get; set; }
        public string ExtraRequirement { get; set; }

        //加工参数
        public double Diameter1 { get; set; }
        public double Diameter2 { get; set; }

        public double Thickness1 { get; set; }
        public double Thickness2 { get; set; }
        public double Thickness3 { get; set; }
        public double Thickness4 { get; set; }

        public string Defects { get; set; }
    }
}
