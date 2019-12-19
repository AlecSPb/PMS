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
                ShowMessage("产品ID不能为空");
                return;
            }

            if (productid.Length < 10)
            {
                ShowMessage("产品ID至少应该是10位");
                return;
            }

            P_SearchItem.Inlines.Clear();
            P_SearchItem.Inlines.Add($"搜索ID为[{productid}]");

            var service = new IntegratedSearchService();

            //获取测试结果
            var record = service.GetRecordTestString(productid);
            if (record.IsSucceed)
            {
                P_RecordResult.Inlines.Add(record.Result.ToString());
            }
            else
            {
                ShowMessage("没有找到相关记录，请检查输入ID是否正确");
            }

        }

        private void ShowMessage(string msg)
        {
            MainDoc.Blocks.Clear();
            var p = new Paragraph();
            p.Inlines.Add(msg);
            MainDoc.Blocks.Add(p);
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
    }
}
