using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Simulator
{
    /// <summary>
    /// 用于接收成分模拟输入
    /// </summary>
    public class InputValue
    {
        public InputValue()
        {
            Element = "";
            Ratio = 0;
            Offset = Properties.Settings.Default.XRFOffset;
        }
        public string Element { get; set; }
        public double Ratio { get; set; }
        public double Offset { get; set; }
    }
}
