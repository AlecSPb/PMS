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
using System.Windows.Shapes;
using System.IO;

namespace PMSClient
{
    /// <summary>
    /// UpdateMessages.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateMessages : Window
    {
        public UpdateMessages()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Updates.txt");
                if (File.Exists(updateFile))
                {
                    txtUpdatesMessage.Text = File.ReadAllText(updateFile);
                }
                else
                {
                    txtUpdatesMessage.Text = "出现了一些错误";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
