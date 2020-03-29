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

namespace PMSClient.Components.CscanImageProcess
{
    /// <summary>
    /// ImageTypeSelectionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImageTypeSelectionDialog : Window
    {
        public ImageTypeSelectionDialog()
        {
            InitializeComponent();
        }

        public ImageType SelectedImageType { get; set; }

        private void BtnBonding_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageType = ImageType.Bonding;
            DialogResult = true;
            Close();
        }

        private void BtnTarget_Click(object sender, RoutedEventArgs e)
        {
            SelectedImageType = ImageType.Target;
            DialogResult = true;
            Close();
        }
    }
}
