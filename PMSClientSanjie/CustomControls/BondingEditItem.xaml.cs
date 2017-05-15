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

namespace PMSClient.CustomControls
{
    /// <summary>
    /// BondingEditItem.xaml 的交互逻辑
    /// </summary>
    public partial class BondingEditItem : UserControl
    {
        public BondingEditItem()
        {
            InitializeComponent();
        }
        public bool IsExpanded
        {
            set
            {
                MyExpander.IsExpanded = value;
            }
        }
        public string Title
        {
            set
            {
                txtTitle.Text = value;
            }
        }

        public Panel LeftArea
        {
            set
            {
                leftArea.Content = value;
            }
        }
        public Panel RightArea
        {
           set
            {
                rightArea.Content = value;
            }
        }


    }
}
