using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 材料需求表，由生产经理根据订单表新建而成
    /// </summary>
    public class PMSMaterialNeed
    {
        public Guid Id { get; set; }
        public string Composition { get; set; }
        public string Purity { get; set; }
        public double Weight { get; set; }
        public string Supplier { get; set; }
        public string SpecialNeeds { get; set; }
        public string PMIWorkingNumber { get; set; }


        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
