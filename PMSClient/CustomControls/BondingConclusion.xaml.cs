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

namespace PMSClient.CustomControls
{
    /// <summary>
    /// BondingConclusion.xaml 的交互逻辑
    /// </summary>
    public partial class BondingConclusion : Window
    {
        public BondingConclusion()
        {
            InitializeComponent();
            State = "完成";
            PlateNumber = "";
            Defects = "无";
        }
        public string State { get; set; }
        public string PlateNumber { get; set; }
        public string Defects { get; set; }
    }
}
