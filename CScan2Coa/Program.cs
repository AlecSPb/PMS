#define PROG



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
