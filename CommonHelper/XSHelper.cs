using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class XSHelper
    {
        static XSHelper()
        {
            MessageHelper = new MessageboxHelper();
            FileHelper = new FileHelper();
        }
        public static MessageboxHelper MessageHelper { get; set; }
        public static FileHelper FileHelper { get; set; }
    }
}
