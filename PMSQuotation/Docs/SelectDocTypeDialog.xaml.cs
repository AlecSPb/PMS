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

namespace PMSQuotation.Docs
{
    /// <summary>
    /// SelectDocTypeDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SelectDocTypeDialog : Window
    {
        public SelectDocTypeDialog()
        {
            InitializeComponent();
        }

        public string SelectedDocType { get; set; } = "English_LEON";

        private void BtnChinese_Click(object sender, RoutedEventArgs e)
        {
            SelectedDocType = "Chinese";
            DialogResult = true;
            this.Close();
        }

        private void BtnEnglish_Click(object sender, RoutedEventArgs e)
        {
            SelectedDocType = "English";
            DialogResult = true;
            this.Close();
        }

        private void BtnEnglish_Leon_Click(object sender, RoutedEventArgs e)
        {
            SelectedDocType = "English_LEON";
            DialogResult = true;
            this.Close();
        }
    }
}
