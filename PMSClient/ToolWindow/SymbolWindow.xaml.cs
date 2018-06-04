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
    /// SymbolWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SymbolWindow : Window
    {
        public SymbolWindow()
        {
            InitializeComponent();
        }

        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button)
            {
                Button btn = e.OriginalSource as Button;
                TxtSymbol.Text = btn.Content.ToString();
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(TxtSymbol.Text != ""))
            {
                Clipboard.SetDataObject(TxtSymbol.Text);
            }
        }
    }
}
