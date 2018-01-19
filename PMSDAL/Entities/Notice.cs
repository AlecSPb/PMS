using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class Notice
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public string  Title { get; set; }
        public string  Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
