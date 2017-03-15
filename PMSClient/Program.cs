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
            App app = new PMSClient.App();
            app.InitializeComponent();

#if IsDesktopEdtion
            var firstWindow = new MainDesktop();
#else
            var firstWindow = new MainTablet();
#endif

            app.Run(firstWindow);
        }
    }
}
