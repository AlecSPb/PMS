using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Model
{
    public class Device
    {
        public Guid DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string DeviceCode { get; set; }

        public int TopTemperature { get; set; }

        public int TopPressure { get; set; }

        public int TopDiameter { get; set; }

        public string Remark { get; set; }
    }
}