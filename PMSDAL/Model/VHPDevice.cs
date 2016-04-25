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
        public string DeviceName { get; set; }
        [Required]
        public string DeviceCode { get; set; }
        public int TopTemperature { get; set; }
        public int TopPressure { get; set; }
        public int TopDiameter { get; set; }
        public string State { get; set; }//可用状态
        public string Remark { get; set; }

    }
}
