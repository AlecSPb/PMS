using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public Guid DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string DeviceCode { get; set; }

        public int TopTemperature { get; set; }

        public int TopPressure { get; set; }

        public int TopDiameter { get; set; }

        public string Remark { get; set; }

    }
}
