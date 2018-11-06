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
    public partial class SingleStringSelector : Window
    {
        public SingleStringSelector()
        {
            InitializeComponent();

            SelectedString = "";
            strings = new List<string>();
          }
        private List<string> strings;
        public void SetDataSource(List<string> ds)
        {
            if (ds != null)
            {
                strings.AddRange(ds);
                LstStrings.ItemsSource = strings;
            }
        }
        public void SetDataSource(List<string> ds,string filter)
        {
            if (ds != null)
            {
                var result=ds.Where(i => i.Contains(filter)).Select(i => i);
                strings.AddRange(result);
                LstStrings.ItemsSource = strings;
            }
        }
        public string SelectedString { get; set; }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            SelectedString = LstStrings.SelectedItem.ToString();

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
