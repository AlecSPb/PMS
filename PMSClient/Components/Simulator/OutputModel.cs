using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Simulator
{
    public class OutputModel
    {
        public OutputModel()
        {
            FirstRow = "";
            RowValues = new List<OutputValue>();
            Average = new OutputValue();
        }

        public string FirstRow { get; set; }
        public List<OutputValue> RowValues { get; set; }
        public OutputValue Average { get; set; }
    }
}
