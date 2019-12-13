using Novacode;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.OutsideProcessService;

namespace PMSClient.ReportsHelperNew
{
    public class ReportOutsideProcessSheetOut : ReportBase
    {
        public ReportOutsideProcessSheetOut()
        {

        }

        public override void Output()
        {
            ResetParameters();

            string source = Path.Combine(reportsFolder, "OutsideProcessSheetBack.docx");
            string temp = Path.Combine(reportsFolder, "temp", "OutsideProcessSheetBack.docx");

            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[CreateDate]", createDate ?? "");

                Table table = doc.Tables[0];
                using (var service = new OutsideProcessServiceClient())
                {
                    recordCount = service.GetOutsideProcessUnCompletedBackCount();
                    if (recordCount == 0)
                    {
                        PMSDialogService.Show("没有未完成的任务");
                        return;
                    }

                    pageCount = GetPageCount();

                    int s = 0, t = 0;
                    while (pageIndex < pageCount)
                    {
                        s = pageIndex * pageSize;
                        t = pageSize;
                        var pageData = service.GetOutsideProcessUnCompletedBack(s, t).OrderBy(i=>i.ProductID);
                        foreach (var item in pageData)
                        {
                            Row row = table.InsertRow();
                            row.Cells[0].Paragraphs[0].Append(item.ProductID);
                            row.Cells[1].Paragraphs[0].Append(item.PMINumber);
                            row.Cells[2].Paragraphs[0].Append("和上一片性质类似");
                            row.Cells[3].Paragraphs[0].Append(item.Dimension);
                            row.Cells[4].Paragraphs[0].Append(item.Remark);

                        }


                        pageIndex++;

                    }



                }

                doc.Save();

                File.Copy(temp, wordFileName, true);
                PMSDialogService.Show("生成成功即将打开");
                System.Diagnostics.Process.Start(wordFileName);
                #endregion



            }


        }
    }
}
