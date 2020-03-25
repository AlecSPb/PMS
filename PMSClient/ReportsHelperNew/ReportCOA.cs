using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Novacode;
using System.Drawing;

namespace PMSClient.ReportsHelperNew
{
    public class ReportCOA:ReportBase
    {
        public ReportCOA()
        {

        }

        public override void Output()
        {
            ResetParameters();

            ResetParameters();
            string source = Path.Combine(reportsFolder, "COA200324.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "COA200324.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[CreateDate]", createDate ?? "");

                Table table = doc.Tables[0];
                var service = new RecordBondingServiceClient();
                //获取所有未完成的绑定数据
                var bondings = service.GetUnFinishedRecordBondings()
                                      .OrderBy(i => i.TargetProductID)
                                      .ThenBy(i => i.PlanBatchNumber);
                service.Close();

                int rownumber = 1;
                foreach (var item in bondings)
                {
                    Row row = table.InsertRow();
                    row.Height = 35;
                    row.Cells[0].Paragraphs[0].Append("□").Alignment = Alignment.center;
                    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[1].Paragraphs[0].Append(item.TargetProductID ?? "");
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].Paragraphs[0].Append(item.TargetComposition ?? "");
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[3].Paragraphs[0].Append(item.TargetPMINumber ?? "");
                    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[4].Paragraphs[0].Append(item.TargetDimensionActual ?? "");
                    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[5].Paragraphs[0].Append(item.TargetDefects ?? "");
                    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

                    //添加间隔背景颜色
                    if (rownumber % 2 == 1)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            row.Cells[i].FillColor = Color.LightGray;
                        }
                    }
                    rownumber++;
                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            //PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);

        }
    }
}
