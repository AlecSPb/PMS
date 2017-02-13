using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDesktopClient
{
    public class MessageObject
    {
        public string ViewName { get; set; }
        public object ModelObject { get; set; }
        public bool IsAdd { get; set; }//判定是否添加
    }
}
