using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PMSClient.MainService;
using PMSClient.ToolWindow;

namespace PMSClient.Components.BatchLabels
{
    public class BLService
    {
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
            StringBuilder sb = new StringBuilder();

            using (var test_s = new RecordTestServiceClient())
            {
                foreach (var productid in idlist)
                {
                    var t = test_s.GetRecordTestByProductID(productid).FirstOrDefault();
                    if (t != null)
                    {
                        //sb.AppendLine($"{t.ProductID}");
                        //sb.AppendLine($"{t.Composition}");
                        //sb.AppendLine($"{t.Dimension}");
                        //sb.AppendLine($"{t.Customer}");
                        //sb.AppendLine($"{t.PO}");
                        sb.AppendLine(ViewModel.VMHelper.RecordTestVMHelper.CreateLabel(t));
                        sb.AppendLine("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                        //using (var order_s = new OrderServiceClient())
                        //{
                        //    var o = order_s.GetOrderByPMINumber(t.PMINumber);

                        //}
                    }
                }
            }


            var win = new LabelCopyWindow();
            win.LabelInformation = sb.ToString();
            win.Show();
        }
    }
}
