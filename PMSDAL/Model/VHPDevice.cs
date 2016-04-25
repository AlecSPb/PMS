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
    /// VHPDevice
    /// </summary>
    public class VHPDevice
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string DeviceName { get; set; } //VHP400A
        [Required]
        public string DeviceCode { get; set; }//设备代码 A
        public int TopTemperature { get; set; }//最高温度
        public int TopPressure { get; set; }//最大压力
        public int TopDiameter { get; set; }//最大直径
        public string State { get; set; }//可用状态
        public DateTime BoughtTime { get; set; }//购买时间
        public string Remark { get; set; }

    }
}
