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

namespace PMSQuotation.Tools
{
    /// <summary>
    /// SpecificationEdit.xaml 的交互逻辑
    /// </summary>
    public partial class SpecificationEdit : Window
    {
        public SpecificationEdit()
        {
            InitializeComponent();
        }

        public string GetContactInStrValue()
        {
            return $"{TxtDimension.Text}+{TxtPurity.Text}+{TxtPlate.Text}+" +
                $"{TxtBonding.Text}+{TxtOther.Text}";
        }

        public void SetContactWithStrValue(string str)
        {
            string[] strs = str.Split(new string[] { "+" },StringSplitOptions.None);
            if (strs.Length >= 5)
            {
                TxtDimension.Text = strs[0];
                TxtPurity.Text = strs[1];
                TxtPlate.Text = strs[2];
                TxtBonding.Text = strs[3];
                TxtOther.Text = strs[4];
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void BtnDimensionCircle_Click(object sender, RoutedEventArgs e)
        {
            TxtDimension.Text = "50mm OD x 4mm";
        }
        private void BtnDimensionRect_Click(object sender, RoutedEventArgs e)
        {
            TxtDimension.Text = "80mm x 50mm x 4mm";
        }
        private void BtnPurity_Click(object sender, RoutedEventArgs e)
        {
            TxtPurity.Text = "99.995%";
        }

        private void BtnPlateYes_Click(object sender, RoutedEventArgs e)
        {
            TxtPlate.Text = "Yes";
        }

        private void BtnPlateNo_Click(object sender, RoutedEventArgs e)
        {
            TxtPlate.Text = "No";
        }

        private void BtnBondingYes_Click(object sender, RoutedEventArgs e)
        {
            TxtBonding.Text = "Yes";
        }

        private void BtnBondingNo_Click(object sender, RoutedEventArgs e)
        {
            TxtBonding.Text = "No";
        }


    }
}
