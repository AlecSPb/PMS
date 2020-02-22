using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSHelper.Helpers;

namespace XSHelper
{
    /// <summary>
    /// Helper汇集类，方便调用
    /// </summary>
    public static class XS
    {
        public static AutoSaveHelper AutoSave { get; set; }
        public static DialogHelper Dialog { get; set; }
        public static FileHelper File { get; set; }
        public static HashHelper Hash { get; set; }
        public static MessageboxHelper MessageBox { get; set; }
        public static StatusHelper Status { get; set; }
    }
}
