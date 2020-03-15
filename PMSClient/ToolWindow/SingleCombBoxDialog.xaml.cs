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
    /// SingleCombBoxDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SingleCombBoxDialog : Window
    {
        public SingleCombBoxDialog()
        {
            InitializeComponent();
        }

        public string CurrentSeletedString
        {
            get
            {
                return CboItem.SelectedItem?.ToString();
            }
        }

        public void SetCboDatasource(List<string> ds)
        {
            this.CboItem.ItemsSource = ds;
            CboItem.SelectedIndex = 0;
        }


        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
