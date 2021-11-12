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

namespace PMSAnalysis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadVHP();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "VHP":
                    LoadVHP();
                    break;
                case "Powder":
                    LoadPowder();
                    break;
                default:
                    break;
            }
        }

        private void LoadVHP()
        {
            this.Title = "真空热压机使用情况统计分析 VHP Usage Anlysis";
            CCMain.Content = new VHPPlanAnalysisView();
        }

        private void LoadPowder()
        {
            this.Title = "制粉情况统计分析 Powder Anlysis";
            CCMain.Content = null;
        }


    }
}
