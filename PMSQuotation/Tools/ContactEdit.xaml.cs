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
using PMSQuotation.Services;

namespace PMSQuotation.Tools
{
    /// <summary>
    /// ContactEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ContactEdit : Window
    {
        public ContactEdit()
        {
            InitializeComponent();
        }
        public string GetContactInStrValue()
        {
            return $"{TxtCompanyName.Text}+{TxtContactPerson.Text}+{TxtPhone.Text}+" +
                $"{TxtEmail.Text}+{TxtAddress.Text}";
        }

        public void SetContactWithStrValue(string str)
        {
            string[] strs = str.Split(new string[] { "+" }, StringSplitOptions.None);
            if (strs.Length >= 5)
            {
                TxtCompanyName.Text = strs[0];
                TxtContactPerson.Text = strs[1];
                TxtPhone.Text = strs[2];
                TxtEmail.Text = strs[3];
                TxtAddress.Text = strs[4];
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void BtnChinese_Click(object sender, RoutedEventArgs e)
        {
            LoadFromDataDictByKey("contactInfo_self_zh_cn");
        }

        private void BtnEnglish_Click(object sender, RoutedEventArgs e)
        {
            LoadFromDataDictByKey("contactInfo_self_us_en");
        }
        private void BtnEmpty_Click(object sender, RoutedEventArgs e)
        {
            LoadFromDataDictByKey("contactInfo_empty");
        }
        private void LoadFromDataDictByKey(string key)
        {
            try
            {
                var db_service = new QuotationDbService();
                var contactinfo_self = db_service.GetDataDictByKey(key).DataValue;
                SetContactWithStrValue(contactinfo_self);
            }
            catch (Exception ex)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }
        }
    }
}
