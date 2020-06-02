using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Novacode;
using PMSClient.ExtraService;
using PMSClient.ToolService;

namespace PMSClient.ReportsHelperNew
{
    public class ReportMillingTool : ReportBase
    {
        public ReportMillingTool()
        {

        }

        public override void Output()
        {
            ResetParameters();
            string source = Path.Combine(reportsFolder, "ToolMillingList.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "ToolMillingList.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[PrintDate]", createDate ?? "");

                Table table = doc.Tables[0];
                using (var service = new ToolSieveServiceClient())
                {
                    recordCount = service.GetToolSieveCount(empty,empty, empty);
                    pageCount = GetPageCount();

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetToolSieve(empty,empty, empty, s, t);
                        var ordered = pageData;
                        foreach (var item in ordered)
                        {
                            Row row = table.InsertRow();
                            row.Height = 35;
                            row.Cells[0].Paragraphs[0].Append(item.BoxNumber.ToString()).Alignment= Alignment.center;
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[1].Paragraphs[0].Append(item.SearchID.ToString()).Alignment= Alignment.center;
                            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[2].Paragraphs[0].Append(item.MaterialGroup ?? "");
                            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[3].Paragraphs[0].Append(item.Specification ?? "");
                            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[4].Paragraphs[0].Append(item.Manufacture ?? "");
                            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[5].Paragraphs[0].Append(item.StartTime.ToShortDateString() ?? "");
                            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[6].Paragraphs[0].Append(item.StopTime.ToShortDateString() ?? "");
                            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;

                            row.Cells[7].Paragraphs[0].Append(item.Remark ?? "");
                            row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
                        }

                        pageIndex++;
                    }





                }
                #endregion
                doc.Save();
            }
            File.Copy(temp, wordFileName, true);
            PMSDialogService.Show("生成成功，即将打开");
            System.Diagnostics.Process.Start(wordFileName);
        }

    }
}
