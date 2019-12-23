using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PMSClient.IntegratedSearch;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// IntegratedSearch.xaml 的交互逻辑
    /// </summary>
    public partial class IntegratedSearch : Window
    {
        public IntegratedSearch()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string productid = TxtProductID.Text.Trim();
            if (string.IsNullOrEmpty(productid))
            {
                ChangeSearchItem("产品ID不能为空");
                return;
            }

            if (productid.Length < 10)
            {
                ChangeSearchItem("产品ID至少应该是10位");
                return;
            }

            ChangeSearchItem($"搜索ID为[{productid}]");


            var service = new IntegratedSearchService();
            P_RecordResult.Inlines.Add(service.GetSearchResult(productid));
        }

        private void ChangeSearchItem(string msg)
        {

            P_SearchItem.Inlines.Clear();
            P_SearchItem.Inlines.Add(msg);
        }

        /// <summary>
        /// 获取产品id前8位作为搜索字符串
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        private string GetSearchString(string productid)
        {
            return productid.Trim().Substring(0, 8);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = TxtProductID.Text;
            saveFile.Filter = "文本|*.txt";

            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextRange range = new TextRange(MainDoc.ContentStart,
                    MainDoc.ContentEnd);
                File.WriteAllText(saveFile.FileName, range.Text);
                PMSDialogService.Show("保存成功");
            }

        }
    }
}
