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
            //Paragraph p = CreateTitle("测试记录");

            //FlowDocument document = new FlowDocument();
            //document.Blocks.Add(p);
            //MainDoc.Document = document;
            P_SearchItem.Inlines.Clear();
            P_SearchItem.Inlines.Add("hello world");
        }

        /// <summary>
        /// 创建一个Paragraph类型的title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private Paragraph CreateTitle(string title)
        {
            Paragraph p = new Paragraph();
            Run t = new Run(title);
            p.Inlines.Add(t);
            return p;
        }

        /// <summary>
        /// 创建内容类型
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private Paragraph CreateContent(string content)
        {
            Paragraph p = new Paragraph();
            Run t = new Run(content);
            p.Inlines.Add(t);
            return p;
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
