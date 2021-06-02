using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.Text.RegularExpressions;
using Novacode;
using System.IO;

namespace PMSClient.Components.RecordTestCheck
{
    /// <summary>
    /// 检测检查单生成服务
    /// </summary>
    public class RTCService
    {
        //更新状态时间
        public event EventHandler<string> UpdateStatus;

        //生成检查单文件
        public void Generate(string idstrs)
        {
            if (string.IsNullOrEmpty(idstrs))
            {
                XSHelper.XS.MessageBox.ShowInfo("输入靶材ID不能为空");
                return;
            }
            string pattern = @"\d{6}-\w{1,2}-\w+";

            string[] productids = idstrs.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> idlist = new List<string>();

            foreach (var id_str in productids)
            {
                string p = id_str.Trim();
                bool ismatch = Regex.IsMatch(p, pattern);
                if (ismatch)
                {
                    idlist.Add(p);
                }
            }



            //生成文档
            string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                                $"检测检查单文件{DateTime.Today.ToString("yyMMdd")}.docx");

            var doc = DocX.Create(filename);
            doc.InsertParagraph($"{DateTime.Today.ToShortDateString()}靶材准备检查单");

            using (var test_s=new RecordTestServiceClient())
            {
                foreach (var productid in idlist)
                {
                    var t = test_s.GetRecordTestByProductID(productid).FirstOrDefault();
                    if (t != null)
                    {
                        using (var order_s=new OrderServiceClient())
                        {
                            var o = order_s.GetOrderByPMINumber(t.PMINumber);
                            doc.InsertParagraph(productid + " " + t.Composition+" "+o.SpecialRequirement);

                            UpdateStatus?.Invoke(this,$"已处理:[{t.ProductID}]");
                        }
                    }
                }
            }


            doc.Save();

            XSHelper.XS.MessageBox.ShowInfo("文档保存成功");
            try
            {
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception)
            {
            }
        }


    }
}
