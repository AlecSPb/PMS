using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Docs
{
    public class DocOptions
    {
        public DocOptions()
        {
            DocType = "English";
        }
        public string DocType { get; set; }
    }
}
