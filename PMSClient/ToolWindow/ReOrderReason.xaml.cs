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
using PMSCommon;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// ReOrderReason.xaml 的交互逻辑
    /// </summary>
    public partial class ReOrderReason : Window
    {
        public ReOrderReason()
        {
            InitializeComponent();
            List<string> list = new List<string>();
            PMSBasicDataService.SetListDS(CustomData.MaterialReOrderReason, list);
            lst.ItemsSource = list;
            WindowContent = string.Empty;
        }

        public string WindowContent { get; private set; }

        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowContent = lst.SelectedItem.ToString();
            this.Close();
        }
    }
}
