using PMSClient.BasicService;
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

namespace PMSClient.View
{
    /// <summary>
    /// CompoundEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CompoundEditView : UserControl
    {
        public CompoundEditView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string composition = txtCompoundName.Text.Trim();
            using (var service = new CompoundServiceClient())
            {
                if (service.GetCompoundCount(composition) > 0)
                {
                    PMSDialogService.Show($"已经存在[{composition}]，无需继续添加");
                }
                else
                {
                    PMSDialogService.Show($"可以添加[{composition}]");

                }
            }
        }
    }
}
