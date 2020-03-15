using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Simulator
{
    public class InputModel
    {
        public InputModel()
        {
            RecordCount = 5;
            Elements = new List<InputValue>();
        }

        public int RecordCount { get; set; }
        public List<InputValue> Elements { get; set; }
    }
}
