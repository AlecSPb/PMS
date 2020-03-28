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

namespace WPFControls
{
    /// <summary>
    /// NormalizedDataViewer.xaml 的交互逻辑
    /// </summary>
    public partial class NormalizedDataViewer : Window
    {
        public NormalizedDataViewer()
        {
            InitializeComponent();
        }
        public void SetMainStrings(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            lst.Items.Clear();
            lst.ItemsSource = NormalizedDataHelper.Analysis(s);
        }

    }
}
