using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSLargeScreen.PMSMainService;

namespace PMSLargeScreen
{
    public class SinglePanelModel
    {
        public string DeviceCode { get; set; }
        public string MoldType { get; set; }
        public double MoldDiameter { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double Vaccum { get; set; }
        public double PrePressure { get; set; }
        public double PreTemperature { get; set; }
        public double PreVaccum { get; set; }
        public string ProcessCode { get; set; }

        public List<string> Compositions { get; set; }
    }
}
