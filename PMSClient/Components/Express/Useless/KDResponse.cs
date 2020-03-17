using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express
{
    public class KDResponse
    {
        public KDResponse()
        {
            data = new Data();
        }
        public string message { get; set; }
        public int state { get; set; }
        public int status { get; set; }
        public string condition { get; set; }
        public int ischeck { get; set; }
        public string com { get; set; }
        public string nu { get; set; }


        public Data data { get; set; }
    }

    public class Data
    {
        public string context { get; set; }
        public string time { get; set; }
        public string ftime { get; set; }
        public string status { get; set; }
        public string areaCode { get; set; }
        public string areaName { get; set; }

    }
}
