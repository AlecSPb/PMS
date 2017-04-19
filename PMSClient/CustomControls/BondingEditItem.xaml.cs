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



        public UserControl TopArea
        {
            get { return (UserControl)GetValue(TopAreaProperty); }
            set { SetValue(TopAreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopArea.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopAreaProperty =
            DependencyProperty.Register("TopArea", typeof(UserControl), typeof(BondingEditItem), new PropertyMetadata(null,new PropertyChangedCallback(ActionTopAreaChanged)));

        private static void ActionTopAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
