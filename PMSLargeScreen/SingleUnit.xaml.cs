using PMSLargeScreen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PMSLargeScreen
{
    /// <summary>
    /// SingleUnit.xaml 的交互逻辑
    /// </summary>
    public partial class SingleUnit : UserControl
    {
        public SingleUnit()
        {
            InitializeComponent();
            //设计模式下显示数据
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                CurrentModel = new UnitModel();
            }
        }


        public UnitModel CurrentModel
        {
            get { return (UnitModel)GetValue(CurrentModelProperty); }
            set { SetValue(CurrentModelProperty, value); }
        }

        public static readonly DependencyProperty CurrentModelProperty =
            DependencyProperty.Register("CurrentModel", typeof(UnitModel), typeof(SingleUnit), new PropertyMetadata(PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SingleUnit source = (SingleUnit)d;
            //不能直接用整个usercontrol，只能使用部分控件
            source.mainGrid.DataContext = (UnitModel)e.NewValue;
        }
    }
}
