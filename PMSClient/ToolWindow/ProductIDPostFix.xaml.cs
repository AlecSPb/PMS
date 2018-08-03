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
    /// ProductIDPostFix.xaml 的交互逻辑
    /// </summary>
    public partial class ProductIDPostFix : Window
    {
        public ProductIDPostFix()
        {
            InitializeComponent();
            Postfix = string.Empty;
        }

        public string Postfix { get; set; }
        private void WrapPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn != null)
            {
                Postfix = btn.Content.ToString();
                e.Handled = true;
                this.Close();
            }

        }
    }
}
