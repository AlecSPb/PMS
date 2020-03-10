using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class BasicDictionary : ModelBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
