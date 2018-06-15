using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Novacode;
using PMSClient.ExtraService;

namespace PMSClient.ReportsHelperNew
{
    public class ReportFillingTool : ReportBase
    {
        public ReportFillingTool()
        {

        }

        public override void Output()
        {
            ResetParameters();
            string source = Path.Combine(reportsFolder, "ToolFillingList.docx");
            string temp = Path.Combine(reportsFolder, "Temp", "ToolFillingList.docx");
            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[PrintDate]", createDate ?? "");

                Table table = doc.Tables[0];
                using (var service = new ToolInventoryServiceClient())
                {
                    recordCount = service.GetToolFillingsCount(empty, empty);
                    pageCount = GetPageCount();

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetToolFillings(s, t, empty, empty);
                        var ordered = pageData;
                        foreach (var item in ordered)
                        {
                            Row row = table.InsertRow();
                            row.Height = 35;
                            row.Cells[0].Paragraphs[0].Append(item.ToolNumber.ToString() ?? "").Alignment=Alignment.center;
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                            row.Cells[1].Paragraphs[0].Append(item.CompositionAbbr ?? "");
                            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
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
