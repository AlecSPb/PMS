using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMSClient.ToolWindow
{
    public class DialogWindowBase:Window
    {
        public DialogWindowBase()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.ToolWindow;
        }
    }
}
