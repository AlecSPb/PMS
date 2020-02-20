using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    /// <summary>
    /// 静态汇集类
    /// </summary>
    public static class XSHelper
    {
        static XSHelper()
        {
            MessageHelper = new MessageboxHelper();
            FileHelper = new FileHelper();
            DialogHelper = new DialogHelper();
            HashHelper = new HashHelper();
        }
        public static MessageboxHelper MessageHelper { get; set; }
        public static FileHelper FileHelper { get; set; }
        public static DialogHelper DialogHelper { get; set; }
        public static HashHelper HashHelper { get; set; }
    }
}
