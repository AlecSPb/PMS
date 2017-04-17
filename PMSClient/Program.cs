#define IsDesktopEdtion



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient
{

    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                App app = new PMSClient.App();
                app.InitializeComponent();

                 var firstWindow = new MainDesktop();
                app.Run(firstWindow);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
