using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ToolWindow
{
    public class ConfirmModel
    {
        public ConfirmModel()
        {
            MaterialItemLot = "";
            Composition = "";
            PMINumber = "";
            Weight = ActualWeight = 0;
            MaterialSource=MeltingPoint = Remark = "";
        }
        public string MaterialItemLot { get; set; }
        public string Composition { get; set; }
        public string PMINumber { get; set; }
        public double Weight { get; set; }
        public double ActualWeight { get; set; }

        public string MaterialSource { get; set; }
        public string MeltingPoint { get; set; }
        public string Remark { get; set; }

    }
}
