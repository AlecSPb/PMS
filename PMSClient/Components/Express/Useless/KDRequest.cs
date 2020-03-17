using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express
{
    public class KDRequest
    {
        public KDRequest()
        {
            param = new Param();
        }
        public string customer { get; set; }
        public string sign { get; set; }
        public Param param { get; set; }
    }
    public class Param
    {
        public string com { get; set; }
        public string num { get; set; }
        public string phone { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string resultv2 { get; set; }

    }
}
