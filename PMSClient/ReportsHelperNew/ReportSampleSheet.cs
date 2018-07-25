using Novacode;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelperNew
{
    public class ReportSampleSheet : ReportBase
    {
        public ReportSampleSheet()
        {

        }

        public override void Output()
        {
            ResetParameters();

            string source = Path.Combine(reportsFolder, "SampleSheet.docx");
            string temp = Path.Combine(reportsFolder, "temp", "SampleSheet.docx");

            File.Copy(source, temp, true);

            using (var doc = DocX.Load(temp))
            {
                #region 字段
                string createDate = DateTime.Today.ToShortDateString();
                doc.ReplaceText("[CreateDate]", createDate ?? "");

                Table table = doc.Tables[0];
                using (var service = new MissonServiceClient())
                {
                    recordCount = service.GetMissonUnCompletedCountSample();
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
                        var pageData = service.GetMissonUnCompletedSample(s, t);
                        foreach (var item in pageData)
                        {
                            Row row = table.InsertRow();
                            string row_1 = item.CreateTime.ToString("yyyy-MM-dd");
                            row.Cells[0].Paragraphs[0].Append(row_1);
                            row.Cells[1].Paragraphs[0].Append(item.CustomerName);
                            row.Cells[2].Paragraphs[0].Append(item.PMINumber);
                            row.Cells[3].Paragraphs[0].Append(item.CompositionStandard);
                            row.Cells[4].Paragraphs[0].Append(item.SampleNeed);
                            row.Cells[5].Paragraphs[0].Append("□");
                            row.Cells[5].Paragraphs[0].Alignment = Alignment.center;

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
