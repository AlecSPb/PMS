#define PROG
//定义了PROG编译的是 超声照片标记工具，注释掉后编译的是 超声照片添加到COA文件工具


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ImportTargetPhotoIntoReport
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        #if PROG
            Application.Run(new CscanMarker());
        #else
            Application.Run(new CscanToCoa());
        #endif
        }
    }
}
