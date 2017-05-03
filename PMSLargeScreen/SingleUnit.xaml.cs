using PMSLargeScreen.Models;
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
            this.DataContext = CurrentModel;
        }


        public UnitModel CurrentModel
        {
            get { return (UnitModel)GetValue(CurrentModelProperty); }
            set { SetValue(CurrentModelProperty, value); }
        }

        public static readonly DependencyProperty CurrentModelProperty =
            DependencyProperty.Register("CurrentModel", typeof(UnitModel), typeof(SingleUnit), new PropertyMetadata(new UnitModel()));


    }
}
