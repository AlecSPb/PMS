using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express
{
    public class Trace
    {
        public Trace()
        {
            AcceptTime = DateTime.Now;
            AcceptStation = "";
            Remark = "";
        }
        public DateTime AcceptTime { get; set; }
        public string AcceptStation { get; set; }
        public string Remark { get; set; }
    }
}
