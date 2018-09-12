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
    /// MaterialPriceTool.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialPriceTool : Window
    {
        public MaterialPriceTool()
        {
            InitializeComponent();
            Init();
        }
        public event EventHandler<MaterialPriceToolArgs> Fill;

        private List<string> elements;
        private void Init()
        {
            elements = new List<string>()
            {
                "Cu","In","Ga","Se","Bi","Te","Se","Sb","Ag","As","Ge","Si","S","Zn","Ti","Pd","P","Sc"
            };
            elements.Sort();
            CboElements.ItemsSource = elements;
            CboElements.SelectedIndex = 0;
        }

        private double total_price = 0;

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double unit_price, weight;
                double.TryParse(TxtUnitPrice.Text.Trim(), out unit_price);
                double.TryParse(TxtWeight.Text.Trim(), out weight);
                total_price = unit_price * weight;
                TxtTotalPrice.Text = total_price.ToString("F2");
            }
            catch (Exception)
            {
                PMSDialogService.ShowWarning("填写的不是数字吧");
            }
        }

        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            MaterialPriceToolArgs arg = new MaterialPriceToolArgs();
            arg.TotalPrice = total_price;

            arg.ProvideMaterial = $"{CboElements.Text.Trim()}{TxtWeight.Text.Trim()}kg;";
            Fill?.Invoke(this, arg);
        }
    }
}
