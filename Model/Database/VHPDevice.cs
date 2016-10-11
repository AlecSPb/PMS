using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VHPDevice
    {
        public Guid ID { get; set; }
        public string CodeName { get; set; }
        public string DeviceInformation { get; set; }
        public double HighestTemperature { get; set; }
        public double HighestPressure { get; set; }
        public double HighestDiameter { get; set; }
        public int CurrentState { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
