using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.Text.RegularExpressions;
using Novacode;
using System.IO;
using PMSClient.BarCodeService;

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
            Novacode.Paragraph title = doc.InsertParagraph($"{DateTime.Today.ToShortDateString()}靶材准备检查单");
            title.FontSize(16);
            title.Alignment = Alignment.center;
            title.Bold();

            using (var test_s = new RecordTestServiceClient())
            {
                foreach (var productid in idlist)
                {
                    var t = test_s.GetRecordTestByProductID(productid).FirstOrDefault();
                    if (t != null)
                    {
                        using (var order_s = new OrderServiceClient())
                        {
                            var o = order_s.GetOrderByPMINumber(t.PMINumber);




                            Table table = doc.AddTable(4, 3);
                            table.Design = TableDesign.TableGrid;
                            table.Alignment = Alignment.center;
                            table.AutoFit = AutoFit.Window;
                            table.SetColumnWidth(0, 3000);
                            table.SetColumnWidth(1, 3000);
                            table.SetColumnWidth(2, 3000);

                            table.Rows[0].Cells[0].Paragraphs[0].Append(t.ProductID ?? "").FontSize(12).
                                                                                     Bold().Color(System.Drawing.Color.Blue);
                            table.Rows[0].Cells[1].Paragraphs[0].Append(t.Composition ?? "").FontSize(8);
                            table.Rows[0].Cells[2].Paragraphs[0].Append(t.DimensionActual ?? "").FontSize(8);

                            table.Rows[1].Cells[0].Paragraphs[0].Append(o.CustomerName ?? "").FontSize(8);
                            table.Rows[1].Cells[1].Paragraphs[0].Append(o.PO ?? "").FontSize(8);
                            table.Rows[1].Cells[2].Paragraphs[0].Append(o.LaserNeed ?? "").FontSize(6);


                            table.Rows[2].Cells[0].Paragraphs[0].Append("Product ID").FontSize(8);
                            table.Rows[2].Cells[1].Paragraphs[0].Append("Target Laser").FontSize(8);
                            table.Rows[2].Cells[2].Paragraphs[0].Append("Plate Laser").FontSize(8);


                            LaserContent laser = GetLaserContent(t.LaserEngraved);

                            StringBuilder sb = new StringBuilder();
                            sb.Append("已处理:");
                            if (t.ProductID != null)
                            {
                                var pic1 = GetBarCodeImage(doc, t.ProductID);
                                var p = table.Rows[3].Cells[0].Paragraphs[0];
                                p.AppendPicture(pic1).Alignment = Alignment.center;
                                p.AppendLine(t.ProductID).FontSize(8);
                                sb.Append($"产品ID={t.ProductID}");

                            }

                            if (laser.Target != null)
                            {
                                var pic2 = GetBarCodeImage(doc, laser.Target);
                                var p = table.Rows[3].Cells[1].Paragraphs[0];
                                p.AppendPicture(pic2).Alignment = Alignment.center;
                                p.AppendLine(laser.Target).FontSize(8);
                                sb.Append($"靶材激光 ={ laser.Target}");

                            }

                            if (laser.Plate != null)
                            {
                                var pic3 = GetBarCodeImage(doc, laser.Plate);
                                var p = table.Rows[3].Cells[2].Paragraphs[0];
                                p.AppendPicture(pic3).Alignment = Alignment.center;
                                p.AppendLine(laser.Plate).FontSize(8);
                                sb.Append($"背板激光 ={ laser.Plate}");


                            }


                            doc.InsertParagraph().InsertTableAfterSelf(table);

                            UpdateStatus?.Invoke(this, sb.ToString());
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


        private Novacode.Picture GetBarCodeImage(DocX doc, string bcStr)
        {
            var helper = new QRCodeHelper();
            //var img= helper.CreateQRCodeBmp(t.ProductID);
            var img = helper.CreateQRCodeImage(bcStr);

            Novacode.Image imgInsert = doc.AddImage(img);
            var pic = imgInsert.CreatePicture();

            int fix_size = 100;
            pic.Width = fix_size;
            pic.Height = fix_size;

            return pic;
        }

        /// <summary>
        /// 处理激光雕刻字段
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private LaserContent GetLaserContent(string s)
        {
            LaserContent result = new LaserContent();
            if (!(string.IsNullOrEmpty(s) && s.Contains("无")))
            {
                //如果不包含+号就是只有靶材
                if (!s.Contains("+"))
                {
                    result.Target = s;
                }//如果以+号开头就是只有背板
                else if (s.StartsWith("+"))
                {
                    result.Plate = s.Substring(1, s.Length - 1);

                }
                else
                {
                    string[] strs = s.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                    if (strs.Length >= 2)
                    {
                        result.Target = strs[0];
                        result.Plate = strs[1];
                    }
                }

            }
            return result;
        }

    }
}
