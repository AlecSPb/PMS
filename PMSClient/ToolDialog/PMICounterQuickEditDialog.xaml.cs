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

namespace PMSClient.ToolDialog
{
    /// <summary>
    /// PMICounterQuickEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PMICounterQuickEditDialog : Window
    {
        public PMICounterQuickEditDialog()
        {
            InitializeComponent();
        }

        public double Counter
        {
            get
            {
                return double.Parse(TxtCount.Text.Trim());
            }
        }

        public string Remark
        {
            get { return TxtRemark.Text; }
            set
            {
                TxtRemark.Text = value;
            }
        }

        public PMICounterEditType EditType { get; set; } = PMICounterEditType.IsCancel;

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != null)
            {
                if (btn.Name == "BtnAdd")
                {
                    EditType = PMICounterEditType.IsAdd;
                }
                else
                {
                    EditType = PMICounterEditType.IsEdit;
                }
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            EditType = PMICounterEditType.IsCancel;
            this.Close();
        }
    }
}
