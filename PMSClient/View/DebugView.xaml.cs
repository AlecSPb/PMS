using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace PMSClient.View
{
    /// <summary>
    /// DebugView.xaml 的交互逻辑
    /// </summary>
    public partial class DebugView : UserControl
    {
        public DebugView()
        {
            InitializeComponent();
        }


        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder("Log");
        }

        private static void OpenFolder(string folderName)
        {
            string folder = System.IO.Path.Combine(Environment.CurrentDirectory, folderName);
            if (Directory.Exists(folder))
            {
                System.Diagnostics.Process.Start(folder);
            }
        }

        private void btnError_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder("Error");
        }
    }
}
