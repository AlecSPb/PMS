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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace PMSClient.View
{
    /// <summary>
    /// OrderEditView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderEditView : UserControl
    {
        public OrderEditView()
        {
            InitializeComponent();
        }

        private void btnTipCompositionStd_Click(object sender, RoutedEventArgs e)
        {
            PMSMessageBox msg = new PMSClient.PMSMessageBox();
            try
            {
                msg.MessageTitle = "规范的成分写法";
                var filepath = System.IO.Path.Combine(PMSFolderPath.Documents, "stdcomposition.txt");
                var content = File.ReadAllText(filepath);
                msg.MessageContent = content;
                msg.ShowDialog();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }


        }
    }
}
