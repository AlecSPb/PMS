using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ToolWindow
{
    public class MaterialPriceToolArgs:EventArgs
    {
        public double TotalPrice { get; set; }
        public string ProvideMaterial { get; set; }
    }
}
