using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class FeedBack
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string ProductID { get; set; }
        public string ProductType { get; set; }
        public string Composition { get; set; }
        public string Customer { get; set; }
        public string Problem { get;  set; }
        public string ProcessWay { get; set; }
        public string Remark { get; set; }
    }
}
