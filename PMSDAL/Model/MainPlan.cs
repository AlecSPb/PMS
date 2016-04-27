using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    /// <summary>
    /// 主热压计划
    /// </summary>
    public class MainPlan
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid MainOrderId { get; set; }
        [Required]
        public DateTime?  VHPTime { get; set; }//热压日期，手动制定，后期Lot号的重要参考

        public string VHPType { get; set; }//

        public string DeviceType { get; set; }//设备类型，用设备的编号代替

        public string ProcessCode { get; set; }//热压工艺代码，W1，W2，F1等

        public double MoldDiameter { get; set; }//模具内径

        public double Thickness { get; set; }//热压厚度

        public int Quantity { get; set; }//热压数量

        public double DensityCal { get; set; }//计算用的密度

        public string Pressure { get; set; }//最高压力

        public string Temperature { get; set; }//最高温度

        public string Vaccum { get; set; }//要求真空度

        public string KeepTime { get; set; }//热压时间

        public string FillRequirement { get; set; }//装模要求，要不要石墨纸，Al2O3纸等等

        public string PersonInCharge { get; set; }//负责人

        public string State { get; set; }//计划状态，可以显示和隐藏当前计划，而不用删除它

        public string Remark { get; set; }//备注信息


    }
}
