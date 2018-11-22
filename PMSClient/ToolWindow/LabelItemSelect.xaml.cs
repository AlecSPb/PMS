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

    public class LabelItemSelectResult
    {
        public bool HasSeperator { get; set; } = true;
        public bool HasProductID { get; set; } = true;
        public bool HasComposition { get; set; } = true;
        public bool HasCustomer { get; set; } = true;
        public bool HasDimension { get; set; } = true;
        public bool HasPO { get; set; } = true;
        public bool HasPlateLot { get; set; } = true;

    }
    /// <summary>
    /// LabelItemSelect.xaml 的交互逻辑
    /// </summary>
    public partial class LabelItemSelect : Window
    {
        public LabelItemSelect()
        {
            InitializeComponent();
        }
        private LabelItemSelectResult result = new LabelItemSelectResult();

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            result.HasProductID = (bool)ChkProductID.IsChecked;
            result.HasComposition = (bool)ChkComposition.IsChecked;
            result.HasDimension = (bool)ChkDimension.IsChecked;
            result.HasCustomer = (bool)ChkCustomer.IsChecked;
            result.HasPO = (bool)ChkPO.IsChecked;
            result.HasPlateLot = (bool)ChkPlateLot.IsChecked;
            result.HasSeperator = (bool)ChkSeperator.IsChecked;
            this.DialogResult = true;
        }

        public LabelItemSelectResult Result
        {
            get
            {
                return result;
            }
        }

    }
}
