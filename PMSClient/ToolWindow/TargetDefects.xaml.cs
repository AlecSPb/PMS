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

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// TargetDefects.xaml 的交互逻辑
    /// </summary>
    public partial class TargetDefects : Window
    {
        public TargetDefects()
        {
            InitializeComponent();

            AllDefects = "无明显缺陷";

            var defects = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestDefectsTypes>(defects);
            LstDefects.ItemsSource = defects;


        }

        public string AllDefects { get; set; }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in LstDefects.SelectedItems)
            {
                sb.Append(item.ToString());
                sb.Append(";");
            }

            AllDefects = sb.ToString();

            this.DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
